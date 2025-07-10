// create the prototype on the String object
//added by MN on 11/1 to trim the spaces
String.prototype.trim = function() 
{
 // skip leading and trailing whitespace
 // and return everything in between
 //return this.replace(/^\s*(\b.*\b|)\s*$/, "$1");
  return this.replace(/(^\s *)|(\s*$)/g,"")
}

function CheckFields(FldName,FldType,maxChar,displayName)
  {
 
  var s;
  Field = eval("document.aspnetForm."+FldName)
//  alert(Field)
  Field.value=Field.value.trim();  
  if(FldType=='text')
  {
  if(Field.value=="" || Field.value.length > maxChar)
  {
    if(Field.value=="") 
		alert(displayName+" cannot have blank or invalid characters.")
	else	
		alert(displayName+" can have maximum of "+maxChar+" characters.")
  Field.focus()
  return false
  }}
  
  else if(FldType=='fldsize')
  {
  if(Field.value.length > maxChar)
  {
  alert(displayName+" cannot be greater than "+maxChar)
  Field.focus()
  return false
  }}

  else if(FldType=='logintext')
  {
  if(Field.value=="" || Field.value.length > maxChar)
  {
    if(Field.value=="") 
		alert(displayName+" cannot be blank.")
	else	
		alert(displayName+" can have maximum of "+maxChar+" characters.")
  Field.focus()
  return false
  }
  if(Field.value.indexOf("@")>=0 || Field.value.indexOf(".")>=0 || Field.value.indexOf("'")>=0 || Field.value.indexOf('"')>=0 || Field.value.indexOf("~")>=0 || Field.value.indexOf("!")>=0 || Field.value.indexOf("#")>=0 || Field.value.indexOf("$")>=0 || Field.value.indexOf("%")>=0 || Field.value.indexOf("^")>=0 || Field.value.indexOf("&")>=0 || Field.value.indexOf("*")>=0 || Field.value.indexOf("(")>=0 || Field.value.indexOf(")")>=0 || Field.value.indexOf("-")>=0 || Field.value.indexOf("+")>=0 || Field.value.indexOf("=")>=0 || Field.value.indexOf("|")>=0 || Field.value.indexOf("/")>=0 || Field.value.indexOf("?")>=0 || Field.value.indexOf(">")>=0 || Field.value.indexOf("<")>=0 || Field.value.indexOf(",")>=0 || Field.value.indexOf(";")>=0 || Field.value.indexOf(":")>=0 || Field.value.indexOf("{")>=0 || Field.value.indexOf("}")>=0 || Field.value.indexOf("[")>=0 || Field.value.indexOf("]")>=0 || Field.value.indexOf("`")>=0 || Field.value.indexOf("\\")>=0)
  {
  alert("Please enter a valid "+displayName+".\n\nOnly characters, numbers and underscores ( _ ) are allowed."); 	   
  Field.focus()
  return false  }
  }
  
  else if(FldType=='textarea')
  {
  if(Field.value=="" || Field.value.length>maxChar)
  {
  	if(Field.value=="") 
		alert(displayName+" cannot be blank.")
	else	
		alert(displayName+" can have maximum of "+maxChar+" characters.")
  Field.focus()
  return false  }
  }
  else if(FldType=='number')
  //{if(isNaN(Field.value))
  // Modified by Jeet on 28 Jul 2006 #2785
  {if(!IsNumeric(Field.value, 0))
  {
  alert("Please enter numeric value for "+displayName+".")
  Field.focus()
  return false  }
  }
  else if(FldType=='numberblank')
  {
	  if(Field.value=="")
	  {
	  alert(displayName+" cannot have blank or invalid characters.")
	  Field.focus()
	  return false  }
	   // Modified by Jeet on 28 Jul 2006 #2785
	  if(!IsNumeric(Field.value, 0))
	  {
	  alert("Please enter numeric value for "+displayName+".")
	  Field.focus()
	  return false  }
  }
  
  else if(FldType=='percent')
  {
  if(Field.value > 100)
  {
  alert("Please enter a value not more than 100 for "+displayName+".")
  Field.focus()
  return false  }
  }

  else if(FldType=='decimal')
  {
  	str = Field.value;
	re = /^\d*\.{0,1}\d+$/;
	pos = str.search(re);
	if (pos < 0) {
		alert("Please enter only Numeric or Decimal values for "+displayName+".")
		Field.focus()
		return false } 
  }	
  
  else if(FldType=='date') 
  {
  DateFld=new Date(Field.value)
  //alert(DateFld)
  if (isNaN(DateFld)) 
  {
  alert("Please enter proper "+displayName+".")
  Field.focus()
  return false }
  }
  else if(FldType=='email')
  {
  //Changed by KS on 12/09/2002
  if(Field.value.indexOf("@")<=0 || Field.value.indexOf("@@")>=0 || Field.value.indexOf(".")<=0 || Field.value.indexOf("..")>=0 || Field.value.indexOf(" ")!=-1 || Field.value.length<=6 || Field.value.indexOf("'")>=0 || Field.value.indexOf('"')>=0 || Field.value.indexOf("~")>=0 || Field.value.indexOf("!")>=0 || Field.value.indexOf("#")>=0 || Field.value.indexOf("$")>=0 || Field.value.indexOf("%")>=0 || Field.value.indexOf("^")>=0 || Field.value.indexOf("&")>=0 || Field.value.indexOf("*")>=0 || Field.value.indexOf("(")>=0 || Field.value.indexOf(")")>=0 || Field.value.indexOf("--")>=0 || Field.value.indexOf("+")>=0 || Field.value.indexOf("=")>=0 || Field.value.indexOf("|")>=0 || Field.value.indexOf("/")>=0 || Field.value.indexOf("?")>=0 || Field.value.indexOf(">")>=0 || Field.value.indexOf("<")>=0 || Field.value.indexOf(",")>=0 || Field.value.indexOf(";")>=0 || Field.value.indexOf(":")>=0 || Field.value.indexOf("{")>=0 || Field.value.indexOf("}")>=0 || Field.value.indexOf("[")>=0 || Field.value.indexOf("]")>=0 || Field.value.indexOf("`")>=0 || Field.value.indexOf("\\")>=0)  
  {
  alert("Please enter a valid "+displayName+"."); 	   
  Field.focus()
  return false  }
  }
 }

function extractDigits(mystr)
{
	// extracts only digits from a string and returns a new numeric string
/*	var newstr = '';
	for( i=0; i<mystr.length; i++ )
	{
	//convert the i-th character to ascii code value
	c = mystr.charCodeAt(i); 
	if( (c>=48) && (c<=57) ) newstr = newstr.concat(mystr.substr(i,1));
	}
	return newstr*/
	return mystr.replace(/[^0-9]/g,'');
}

function validate_date(date_field, desc) {
		date_field = eval("document.aspnetForm."+date_field)		
        if (!date_field.value)  
                return true;
        var in_date = stripCharString(date_field.value," ");
        in_date = in_date.toUpperCase();
        var date_is_bad = 0;  
        if (!allowInString(in_date,"/0123456789T+-"))
                date_is_bad = 1; // invalid characters in date
        if (!date_is_bad) { 
                var has_rdi = 0;
                if (in_date.indexOf("T") >= 0){ 
                        has_rdi = 1;
                }
                if (!date_is_bad && has_rdi && (in_date.indexOf("T") != 0)) { 
                        date_is_bad = 2; // relative date index character is not in first position
                }
                if (!date_is_bad && has_rdi && (in_date.length == 1)) { 
                        var d = new Date();
						var return_month = parseInt(d.getMonth() + 1).toString();
						return_month = (return_month.length==1 ? "0" : "") + return_month; 
						var return_date =  parseInt(d.getDate()).toString();
						return_date = (return_date.length==1 ? "0" : "") + return_date; 
				        in_date = return_date + "/" + return_month + "/" + get_full_year(d);		
                        has_rdi = 0; // date doesn't have rdi char anymore (will also cause failure of add'l rdi checks, which is a good thing)
                }
                if (!date_is_bad && has_rdi && (in_date.length > 1) && !(in_date.charAt(1) == "+" || in_date.charAt(1) == "-")) {
                        date_is_bad = 3; // length of rdi string is greater than 1 but second char is not "+" or "-"
                }
                if (!date_is_bad && has_rdi && isNaN(parseInt(in_date.substring(2,in_date.length),10))) {
                        date_is_bad = 4; // rdi value is not a number
                }
                if (!date_is_bad && has_rdi && (parseInt(in_date.substring(2,in_date.length),10) < 0)) {
                        date_is_bad = 5; // rdi value is not a positive integer
                
				}
                if (!date_is_bad && has_rdi) {
                        var d = new Date();
                        ms = d.getTime();
                        offset = parseInt(in_date.substring(2,in_date.length),10);
                        if(in_date.charAt(1) == "+") {
                                ms += (86400000 * offset);
                        } else {
                                ms -= (86400000 * offset);
                        }
                        d.setTime(ms);
						var return_month = parseInt(d.getMonth() + 1).toString();
						return_month = (return_month.length==1 ? "0" : "") + return_month; 
						var return_date =  parseInt(d.getDate()).toString();
						return_date = (return_date.length==1 ? "0" : "") + return_date; 
				        in_date = return_date + "/" + return_month + "/" + get_full_year(d);	
                        has_rdi = 0;
                }
        } 
        if (!date_is_bad) {
                var date_pieces = new Array();
                date_pieces = in_date.split("/");
                if (date_pieces.length == 2) {
                        var d = new Date();
                        in_date = in_date + "/" + get_full_year(d);
                        date_pieces = in_date.split("/");
                }
                if (date_pieces.length != 3 || parseInt(date_pieces[0],10) < 1 || parseInt(date_pieces[0],10) > 12 
                                || parseInt(date_pieces[1],10) < 1 || parseInt(date_pieces[1],10) > 31 
                                || (date_pieces[2].length != 2 && date_pieces[2].length != 4)) {
                        date_is_bad = 6;  // date is not in format of m[m]/d[d]/yy[yy]
                }
        }
        if (date_is_bad) {
                alert(desc + " must be in the format of DD/MM/YYYY.");
                date_field.focus();
                return (false);
        }
        
        var ms = Date.parse(in_date);
        var d = new Date();
        d.setTime(ms);
		var return_date = d.toLocaleString();
		var return_month = parseInt(d.getMonth() + 1).toString();
		return_month = (return_month.length==1 ? "0" : "") + return_month; 
		var return_date =  parseInt(d.getDate()).toString();
		return_date = (return_date.length==1 ? "0" : "") + return_date; 
        return_date = return_date + "/" + return_month + "/" + get_full_year(d);
        date_field.value = return_date;
        return true;
}       // normalize the year to yyyy
function get_full_year(d) {
		var y = ""
		if (d.getFullYear() != null)
		{
			y = d.getFullYear();
			if (y < 1970) y+= 100;		
		} else
		{	
	        y = d.getYear();
	        if (y > 69  && y < 100) y += 1900;
	        if (y < 1000) y += 2000;
		}
        return y;
}
// The following functions were written by Gordon McComb
// More information can be found here: http://www.javaworld.com/javaworld/jw-02-1997/jw-02-javascript.html
function stripCharString (InString, CharString)  {
        var OutString="";
   for (var Count=0; Count < InString.length; Count++)  {
        var TempChar=InString.substring (Count, Count+1);
      var Strip = false;
      for (var Countx = 0; Countx < CharString.length; Countx++) {
        var StripThis = CharString.substring(Countx, Countx+1)
         if (TempChar == StripThis) {
                Strip = true;
            break;
         }
      }
      if (!Strip)
        OutString=OutString+TempChar;
   }
        return (OutString);
}
function allowInString (InString, RefString)  {
        if(InString.length==0) return (false);
        for (var Count=0; Count < InString.length; Count++)  {
        var TempChar= InString.substring (Count, Count+1);
      if (RefString.indexOf (TempChar, 0)==-1)  
        return (false);
   }
   return (true);
}

function isNumeric(FormName,FieldName,DisplayName,Required)
{
	var invalidnum = "";
	var arrlen = 0;
	var msg="";
	if(FieldName == "price")
	{
		invalidnum=new Array("-"," ","!","@","#","$","%","^","&","*","(",")","+","=","'",'"',"`","?","/","|","{","}","[","]",";",":","~","<",">",",","q","w","e","r","t","y","u","i","o","p","a","s","d","f","g","h","j","k","l","z","x","c","v","b","n","m","Q","W","E","R","T","Y","U","I","O","P","A","S","D","F","G","H","J","K","L","Z","X","C","V","B","N","M");
		arrlen = 83;
		msg="Please enter numeric or decimal value for " + DisplayName;
	}
	else
	{
		invalidnum=new Array("-"," ","!","@","#","$","%","^","&","*","(",")","+","=","'",'"',"`","?","/","|",".","{","}","[","]",";",":","~","<",">",",","q","w","e","r","t","y","u","i","o","p","a","s","d","f","g","h","j","k","l","z","x","c","v","b","n","m","Q","W","E","R","T","Y","U","I","O","P","A","S","D","F","G","H","J","K","L","Z","X","C","V","B","N","M");
		arrlen = 84;
		msg="Please enter numeric value for " + DisplayName;
	}
	var Field = eval("document." + FormName + "." + FieldName);
	var strphone = Field.value.trim();
	var strlen = strphone.length;
	if (Required=="yes")
	{
		if(strphone=="")
		{
			alert(DisplayName + " can't be left blank");
			Field.focus();
			return false;
		}
		if(strphone.substring(0,1)==" ")
		{
			alert(DisplayName + " first character can't be a space");
			Field.focus();
			return false;
		}
	}
	if (strlen!=0){
		for(var i=0;i<=strlen;i++){
			chr=strphone.charAt(i);
			for(var j=0;j<arrlen;j++){	
				if(invalidnum[j]==chr){
					if (chr==" "){
						alert("Please remove space from for " + DisplayName);
					}else{
						alert(msg);
					}
					Field.focus();
					return false;
				}//end of if	
			}//end of inner for		
		}//end of outer for
		
	}//end of outer if
	
	return true;
}

function isCurrency(FormName,FieldName,DisplayName,Required)
{
	var invalidnum = "";
	var arrlen = 0;
	var msg="";
	
	invalidnum=new Array("-"," ","!","@","#","$","%","^","&","*","(",")","+","=","'",'"',"`","?","/","|","{","}","[","]",";",":","~","<",">",",","q","w","e","r","t","y","u","i","o","p","a","s","d","f","g","h","j","k","l","z","x","c","v","b","n","m","Q","W","E","R","T","Y","U","I","O","P","A","S","D","F","G","H","J","K","L","Z","X","C","V","B","N","M");
	arrlen = 83;
	msg="Please enter numeric or decimal value for " + DisplayName;
	
	var Field = eval("document." + FormName + "." + FieldName);
	var strphone = Field.value.trim();
	var strlen = strphone.length;
	if (Required=="yes")
	{
		if(strphone=="")
		{
			alert(DisplayName + " can't be left blank");
			Field.focus();
			return false;
		}
		if(strphone.substring(0,1)==" ")
		{
			alert(DisplayName + " first character can't be a space");
			Field.focus();
			return false;
		}
	}
	if (strlen!=0){
		for(var i=0;i<=strlen;i++){
			chr=strphone.charAt(i);
			for(var j=0;j<arrlen;j++){	
				if(invalidnum[j]==chr){
					if (chr==" "){
						alert("Please remove space from for " + DisplayName);
					}else{
						alert(msg);
					}
					Field.focus();
					return false;
				}//end of if	
			}//end of inner for		
		}//end of outer for
		
	}//end of outer if
	
	return true;
}

function populateCategories(getfieldname, setfieldname, fromfld, subId, subsetfieldname, subfromfld, subsubId)
{
	//alert(getfieldname + "-" + setfieldname + "-" + fromfld + "-" + subId + "-" + subsetfieldname + "-" + subfromfld + "-" + subsubId)
	MM_showHideLayers('Layer1','','show',280,300)
	getObj = eval("document.aspnetForm." + getfieldname)
	id = getObj.options[getObj.selectedIndex].value
	setObj = eval("document.aspnetForm." + setfieldname)
	setObj.length = 1	//delete all the index except the default one
	setTimeout("window.open('ShowCategories.asp?fieldname=" + setfieldname + "&from=" + fromfld + "&ID=" + id + "&subID=" + subId + "&subfieldname=" + subsetfieldname + "&subfrom=" + subfromfld + "&subsubID=" + subsubId + "','PopUp','resizable=no,scrollbars=no,width=600,height=500,left=2000,top=160')",250);
}

function addOption(resultname,resultvalue,pflag) 
{
	var selObj = eval("document.aspnetForm." + pflag );
	/*
	Added the following code by Jeet on 26 Aug 2005 #2571 Product Group Error 
	Reason : There was some Product group with " 
	Solution: The " is replace with ~ and then again replacing that back to " from ~	
	*/
	re = /~/gi
	resultname=resultname.replace(re, '"')
	// End Here
 
	selObj.options[selObj.length] = new Option(resultname.replace(/__/gi,"'"),resultvalue);
}

function setCategories(setfieldname, subId)
{
	setObj = eval("document.aspnetForm." + setfieldname)
	if (subId > 0)
	{
		for (i=0;i<setObj.length;i++)
		{
			if (setObj.options[i].value==subId)
			{
				setObj.options[i].selected = true 
			}
		}	
	}
}

function MM_reloadPage(init) {  //reloads the window if Nav4 resized
  if (init==true) with (navigator) {if ((appName=="Netscape")&&(parseInt(appVersion)==4)) {
    document.MM_pgW=innerWidth; document.MM_pgH=innerHeight; onresize=MM_reloadPage; }}
  else if (innerWidth!=document.MM_pgW || innerHeight!=document.MM_pgH) location.reload();
}
MM_reloadPage(true);


function MM_findObj(n, d) { //v4.0
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && document.getElementById) x=document.getElementById(n); return x;
}

function MM_showHideLayers() { //v3.0
var i,p,v,obj,args=MM_showHideLayers.arguments;
  for (i=0; i<(args.length-2); i+=3) if ((obj=MM_findObj(args[i]))!=null) { v=args[i+2];
    if (obj.style) { obj=obj.style; v=(v=='show')?'visible':(v='hide')?'hidden':v; }
    obj.top=args[3]; obj.visibility=v; obj.left=args[4];}
}

//Added by KS on 06/17/2005 - Issue# 2464
// Reason: to have proper validation for US and CA zip code format

/* This function check for a valid zip code format for US and Canada
 For US: 5 digits (99999) e.g. 11237
 For CA: 6 alphanumeric (x9x9x9) e.g. N1H3X7
 Required Arguments: 
	1. formname - String - (name of the form whose field is to be validated)
	2. FldName - String - (name of the zipcode field to be validated)
	3. CanadaState - Boolean - (Is the state belongs to Canada - True or False)
 Created by KS on 06/17/2005
*/
function checkzip(formname, FldName, CanadaState)
{
var valid, msg;
country = CanadaState ? "CA" : "US";
Field = eval("document."+formname+"."+FldName)
Field.value = Field.value.replace(/\s/g, "").toUpperCase(); //remove spaces and converting to uppercase
zip = Field.value;

//msg = "Valid " + country + " zip code."

if (country == "US")
{
	msg1 = "Valid format is 5 digits (99999) e.g. 11237"
	if (!IsNumeric(zip,0) || (zip.length != 5)) valid = 0;
//	if (isNaN(zip) || (zip.length != 5)) valid = 0;
}
else
{
	msg1 = "Valid format is 6 digits (x9x9x9) e.g. T4P1N2"
	if (!isNaN(zip) || (zip.length != 6)) 
		valid = 0
	else
	{
		for (i=1; i <= zip.length; i++)
		{
			char = zip.substring(i-1,i)
			if ((i%2)==0)
				{if (isNaN(char)) valid=0;}
			else	
				{if (!isNaN(char)) valid=0;}
		}
	}	
}
if (valid==0) 
	{
	msg = "Invalid " + country + " zip code.\n\n" + msg1
	alert(msg)
	Field.select();
	Field.focus();
	return false	
	}
return true	
}

//Added by KS on 06/27/2005 - Issue# 2490
// Reason: isNAN function treat values '44e44' as numeric as e is used as exponential.
/*
Function: IsNumeric(sText, allowDecimal)
A javascript validation function to check whether the details entered by a user are numeric.
It allows numbers 0 through 9.
Allows or disallows a decimal by sending allowDecimal = 1 or 0
*/
function IsNumeric(sText, allowDecimal)
{
   var ValidChars = "0123456789";
   if (allowDecimal == 1)
	   ValidChars = ValidChars + ".";
   var IsNumber=true;
   var Char;
 
   for (i = 0; i < sText.length && IsNumber == true; i++) 
      { 
      Char = sText.charAt(i); 
      if (ValidChars.indexOf(Char) == -1) 
         {
         IsNumber = false;
         }
      }
   return IsNumber;
}
