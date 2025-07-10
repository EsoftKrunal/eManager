// Select the main list and add the class "hasSubmenu" in each LI that contains an UL
$('ul').each(function(){
  $this = $(this);
  $this.find("li").has("ul").addClass("hasSubmenu");
});
// Find the last li in each level
$('li:last-child').each(function(){
return;
  $this = $(this);return;
  // Check if LI has children
  if ($this.children('ul').length === 0){
    // Add border-left in every UL where the last LI has not children
      $this.closest('ul').css("border-left", "5px solid #2b9de0");
      if ($this.closest('ul').hasClass('notopmargin'))
          $this.closest('ul').css("margin-top", "-30px");
      else
          $this.closest('ul').css("margin-top", "30px");
  } else {
    // Add border in child LI, except in the last one
    $this.closest('ul').children("li").not(":last").css("border-left","5px solid #2b9de0");
    // Add the class "addBorderBefore" to create the pseudo-element :defore in the last li
    $this.closest('ul').children("li").last().children("a").addClass("addBorderBefore");
    // Add margin in the first level of the list
    // $this.closest('ul').css("margin-top","15px");
    // Add margin in other levels of the list
   // $this.closest('ul').find("li").children("ul").css("margin-top","29px");
  };
});
// Add bold in li and levels above
$('ul li').each(function(){
  $this = $(this);
  $this.mouseenter(function(){
    $( this ).children("a").css({"color":"#ff0000"});
  });
  $this.mouseleave(function(){
    $( this ).children("a").css({"color":"#428bca"});
  });
});
// Add button to expand and condense - Using FontAwesome

//$('ul li.hasSubmenu').each(function(){
//  $this = $(this);
//  $this.prepend("<a href='#'><i class='fa fa-bullseye'></i><i style='display:none;' class='fa fa-plus-circle'></i></a>");
//  $this.children("a").not(":last").removeClass().addClass("toogle");
//});

// Actions to expand and consense
//$('ul li.hasSubmenu a.toogle').click(function(){
//  $this = $(this);
//  $this.closest("li").children("ul").toggle("slow");
//  $this.children("i").toggle();
//  return false;
//});


$('.action').click(function (ev) {
    if ($(this).attr('key') != analysisid) {
        $(".actionpanel").slideDown();
    }
    else
        $(".actionpanel").slideToggle();

    analysisid = $(this).attr('key');
    paid = $(this).attr('paid');
    status = $(this).attr('status');

    var $img = $(ev.target);
    var x = ev.pageX + 20;
    var y = ev.pageY + 20;
    $(".actionpanel").css({ "top": y + "px", "left": x + "px" });
    $(".actionpanel a").first().focus();
    over = 1;

    $("#txtcausetext").val($(this).parent().find(".causesummary").first().html());
    $(".actionpanel .editcause").removeClass("hide");
    $(".actionpanel .removecause").removeClass("hide");
    $(".actionpanel .addcorraction").removeClass("hide");
    $(".actionpanel .setrootcause").removeClass("hide");
    
    if (paid == 0)
    {
        $(".actionpanel .editcause").addClass("hide");
        $(".actionpanel .removecause").addClass("hide");
        $(".actionpanel .setrootcause").addClass("hide");
    }
    if (status != 'R') {
        $(".actionpanel .addcorraction").addClass("hide");
    }
    

});
$(".actiontarget .close").click(function () {
    Hide();
});
$("#causeitemfilter").keyup(function () {
    var arg = $(this).val();
    $(".causeitem").each(function (i, o) {
        if ($(o).html().substring(0, arg.length) == arg || $(o).html().toLowerCase() == 'other')
            $(o).show();
        else
            $(o).hide();
    })
});

function animatetoRight(obj, from, to) {
    if (from <= to) {
        obj.style.visibility = 'hidden';
        return;
    }
    else {
        var box = obj;
        box.style.right = from + "px";
        setTimeout(function () {
            animatetoRight(obj, from - 5, to);
        }, 1)
    }
}
function Hide() {
    $(".actiontarget").css("visibility","hidden");
    //animatetoRight(document.getElementById(activepanel), 0, -610);
    activepanel = '';
    $("body").height("");
    $("body").css({ "overflow": "" });
}
function animatetoLeft(obj, from, to) {
    obj.style.visibility = '';
        if (from >= to) {
        return;
    }
    else {
        var box = obj;
        box.style.right = from + "px";
        setTimeout(function () {
            animatetoLeft(obj, from + 5, to);
        }, 1)
    }
}
function Show(_activepanel) {
    activepanel = _activepanel;
    animatetoLeft(document.getElementById(activepanel), -600, 0);
    $("body").height("100%");
    $("body").css({ "overflow": "hidden" });
    HideAddNewCause();
}

//$("#dvaddnewcausetrigger").click(function () {
//    $(this).hide();
//    $("#dvaddnewcause").show();
//    $("#txtcausetext").focus();
    
//});
function HideAddNewCause()
{
    $("#dvaddnewcause").hide();
   // $("#dvaddnewcausetrigger").show();
}
