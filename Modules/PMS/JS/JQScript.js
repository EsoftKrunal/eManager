
//---------------------------------

function ShowSelection(ctl) {
    $.each($(ctl).children(), function (i, O) { $(O).addClass("tdSel"); });
}
function HideSelection(ctl) {
    $.each($(ctl).children(), function (i, O) { $(O).removeClass("tdSel"); });
}
function RegisterForAutoScroll() {
    $.each($(".ScrollAutoReset"), function (i, o) {
        $(o).scroll(function () {
            SetScrollPos(this);
        });
    });
}
function CallAfterRefresh(sender, args) {
    $.each($(".ScrollAutoReset"), function (i, o) {
        SetLastFocus($(this).attr('id'));
    });
    RegisterForAutoScroll();
    HighlightOnHover();
}
// Function will call after Update Panel Post back;
function SetOnLoad() {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(CallAfterRefresh);
    try {
        if (Page_CallAfterRefresh != null)
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Page_CallAfterRefresh);
    } catch (err)
        { }
}
function SetScrollPos(ctl) {
    setCookie(ctl.id, ctl.scrollTop, 1);
}
function HighlightOnHover() {
    $(".newformat tbody tr").mouseover(function () { ShowSelection($(this)); });
    $(".newformat tbody tr").mouseout(function () { HideSelection($(this)); });
}

$(document).ready(function () {
    CallAfterRefresh(null, null);
    RegisterForAutoScroll();
    $(window).load(function () { SetOnLoad(); });
    HighlightOnHover();
});


function DisableMe(ctl) {

    if (typeof (Page_IsValid) == 'undefined' || Page_IsValid == null) {
        DisableAfter($(ctl).attr('id'));
    }
    else {
        if (Page_IsValid) {
            DisableAfter($(ctl).attr('id'));
        }
    }
}
function DisableAfter(id) {
    window.setTimeout(function () {
        //$('#' + id).css('display', 'none');
        $('#' + id).val('Processing..');
        //$('#' + id).attr('disabled', 'disabled')
        $(':submit').attr('disabled', 'disabled')
    }, 20);
}
 