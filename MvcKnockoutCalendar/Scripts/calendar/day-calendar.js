/// <reference path="../jquery-2.0.2.js" />
/// <reference path="../knockout-2.2.1.js" />
/// <reference path="../knockout.mapping-latest.js" />
/// <reference path="../moment.js" />

var viewModel = function ()
{
    var $this = this;
    var d = new Date();
    $this.selectedDate = ko.observable(new Date(d.getFullYear(), d.getMonth(), d.getDate()));
    $this.dateDetails = ko.observableArray();
    $this.appointments = ko.observableArray();
    $this.selectedTaskDetails = ko.observable(new taskDetails(d));
    $this.initializeDateDetails = function ()
    {
        $this.dateDetails.removeAll();
        for (var i = 0; i < 24; i++)
        {
            var d = $this.selectedDate();
            $this.dateDetails.push({
                count: i,
                TaskDetails: new getTaskHolder(i, d)
            });
        }
    }
    $this.getTaskDetails = function (date)
    {
        var submitValue = new Date(date.getFullYear(), date.getMonth(), date.getDate());
        var uri = "/api/Calendar/GetTaskDetails";
        $.get(uri, 'id=' + submitValue.getFullYear() + '-' + (submitValue.getMonth() + 1) + '-' + submitValue.getDate()

        ).done(function (data)
        {
            $this.appointments.removeAll();
            $(data).each(function (index, element)
            {
                $this.appointments.push(new appointment(element, index));
            });
        }).error(function (data)
        {
            alert("Failed to retrieve tasks.");
        });
    }
}

var appointment = function (task, i)
{
    var $this = this;
    $this.id = ko.observable(task.Id);
    $this.starts = ko.observable(new Date(task.Starts));
    $this.ends = ko.observable(new Date(task.Ends));
    $this.title = ko.observable(task.Title);
    $this.details = ko.observable(task.Details);
    var trd = $("#appointments-table tr td");
    var trdIndex = ($this.starts().getHours() * 2) + 2;
    var top = $(trd[trdIndex]).position().top + ($this.starts().getMinutes() / 2) + 1;
    $this.posTop = ko.observable(top + "px");
    $this.posLeft = ko.observable(($(trd[trdIndex]).position().left + 1) + "px");
    var diff = $this.ends() - $this.starts();
    $this.posHeight = ko.observable(((diff / 1000 / 120) - 2) + "px");
    $this.posWidth = ko.observable(($(trd[trdIndex]).outerWidth(false) - 7) + "px");

    $this.editAppointment = function ()
    {
        var selDateTime = new taskDetails($this.starts());
        selDateTime.Id($this.id());
        selDateTime.Starts($this.starts());
        selDateTime.Ends($this.ends());
        selDateTime.Title($this.title());
        selDateTime.Details($this.details());
        vm.selectedTaskDetails(selDateTime);
        $('#currentTaskModal').modal('toggle');
    }
}

var getTaskHolder = function (i, d)
{
    var $this = this;
    $this.TaskDetails = ko.observableArray();
    $this.StartDate = ko.observable(new Date(new Date(d).setMinutes(i * 60)));
    $this.EndDate = ko.observable(new Date(new Date(d).setMinutes(((i + 1) * 60) - 1)));
    $this.TimePeriod = ko.computed(function ()
    {
        var hr = $this.StartDate().getHours() > 12 ? $this.StartDate().getHours() - 12 : $this.StartDate().getHours();
        var amPm = ($this.StartDate().getHours() > 12) ? 'pm' : ($this.StartDate().getHours() == 12) ? 'noon' : 'am';
        return hr + amPm;
    });
    $this.create = function (data)
    {
        var selDateTime = new taskDetails($this.StartDate());
        selDateTime.Starts(data.StartDate());
        selDateTime.Ends(data.EndDate());
        selDateTime.ParentTask(data);
        if (data instanceof getTaskHolder)
        {
            vm.selectedTaskDetails(selDateTime);
            $('#currentTaskModal').modal('toggle');
        }
    };
}

var taskDetails = function (date)
{
    var $this = this;
    $this.Id = ko.observable();
    $this.ParentTask = ko.observable();
    $this.Title = ko.observable("New Task");
    $this.Details = ko.observable();
    $this.Starts = ko.observable(new Date(new Date(date).setMinutes(0)));
    $this.Ends = ko.observable(new Date(new Date(date).setMinutes(59)));
    $this.StartTime = ko.computed({
        read: function ()
        {
            return $this.Starts().toLocaleTimeString();
        },
        write: function (value)
        {
            if (value)
            {
                var dt = new Date($this.Starts().toLocaleDateString() + " " + value);
                $this.Starts(new Date($this.Starts().getFullYear(), $this.Starts().getMonth(), $this.Starts().getDate(), dt.getHours(), dt.getMinutes()));
            }
        }
    });
    $this.EndTime = ko.computed({
        read: function ()
        {
            return $this.Ends().toLocaleTimeString();
        },
        write: function (value)
        {
            if (value)
            {
                var dt = new Date($this.Ends().toLocaleDateString() + " " + value);
                $this.Ends(new Date($this.Ends().getFullYear(), $this.Ends().getMonth(), $this.Ends().getDate(), dt.getHours(), dt.getMinutes()));
            }
        }
    });
    $this.deleteVisibility = ko.computed(function ()
    {
        if ($this.Id() > 0)
        {
            return "visible";
        }
        else
        {
            return "hidden";
        }
    });

    $this.Save = function (data)
    {
        var submitData = ko.mapping.toJS(data)
        submitData.Starts = (submitData.Starts.toLocaleString());
        submitData.Ends = (submitData.Ends.toLocaleString());
        var postUrl = "/api/Calendar/SaveTask";
        $.ajax({
            url: postUrl,
            type: "POST",
            contentType: "text/json",
            data: JSON.stringify(submitData)
        }).done(function (data)
        {
            $('#currentTaskModal').modal('toggle');
            vm.getTaskDetails(vm.selectedDate());
        }).error(function (data)
        {
            alert("Failed to Save Task");
        });
    }

    $this.Delete = function (data)
    {
        var postUrl = "/api/Calendar/" + data.Id();
        $.ajax({
            url: postUrl,
            type: "DELETE"
        }).done(function (data)
        {
            $('#currentTaskModal').modal('toggle');
            vm.getTaskDetails(vm.selectedDate());
        }).error(function (data)
        {
            alert("Failed to Delete Task");
        });
    }
    $this.Cancel = function (data)
    {
        $('#currentTaskModal').modal('toggle');
    }
}

$(document).ready(function ()
{
    $('#inlineDatepicker').datepicker().on('changeDate', function (ev)
    {
        vm.selectedDate(ev.date);
        vm.initializeDateDetails();
        vm.getTaskDetails(ev.date);
    });
    vm = new viewModel();
    vm.initializeDateDetails();
    vm.getTaskDetails(new Date());
    ko.applyBindings(vm);
});
