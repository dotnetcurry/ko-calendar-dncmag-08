ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor)
    {
        //initialize datepicker with some optional options
        var options = allBindingsAccessor().datepickerOptions || {};
        $(element).datepicker(options);

        //when a user changes the date, update the view model
        ko.utils.registerEventHandler(element, "changeDate", function (event)
        {
            var value = valueAccessor();
            if (ko.isObservable(value))
            {
                value(event.date);
            }
        });
    },
    update: function (element, valueAccessor)
    {
        var widget = $(element).data("datepicker");
        //when the view model is updated, update the widget
        if (widget)
        {
            widget.date = ko.utils.unwrapObservable(valueAccessor());
            widget.setValue();
        }
    }

};

ko.bindingHandlers.timepicker = {
    init: function (element, valueAccessor, allBindingsAccessor)
    {
        //initialize timepicker 
        var options = $(element).timepicker();

        //when a user changes the date, update the view model        
        ko.utils.registerEventHandler(element, "changeTime.timepicker", function (event)
        {
            var value = valueAccessor();
            if (ko.isObservable(value))
            {
                value(event.time.value);
            }
        });
    },
    update: function (element, valueAccessor)
    {
        var widget = $(element).data("timepicker");
        //when the view model is updated, update the widget
        if (widget)
        {
            var time = ko.utils.unwrapObservable(valueAccessor());
            widget.setTime(time);
        }
    }
};