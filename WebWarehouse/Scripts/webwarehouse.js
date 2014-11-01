//Set styling to include Font-awesome icons for pNotify!
PNotify.prototype.options.styling = "fontawesome";

var stack_topleft = { "dir1": "down", "dir2": "right", "push": "top" };
var stack_bottomleft = { "dir1": "right", "dir2": "up", "push": "top" };

$(function () {
    //Display error-alerts
    var errorMessage = $("#globalMessages").data('error');
    if (errorMessage) {
        new PNotify({
            title: 'Advarsel!',
            text: errorMessage,
            type: "error",
            addclass: "stack-bottomleft",
            stack: stack_bottomleft
        });
    }
    var successMessage = $("#globalMessages").data('success');
    if (successMessage) {
        new PNotify({
            title: 'WOW!',
            text: successMessage,
            type: "success",
            addclass: "stack-bottomleft",
            stack: stack_bottomleft
        });
    }

    var infoMessage = $("#globalMessages").data('info');
    if (infoMessage) {
        new PNotify({
            title: 'Tips:',
            text: infoMessage,
            type: "info",
            addclass: "stack-bottomleft",
            stack: stack_bottomleft
        });
    }
});


$(function () { // will trigger when the document is ready
    $('.datepicker').datetimepicker({

        
                      //en/disables the seconds picker
    }); //Initialise any date pickers
});