var JQ_DATE_FORMAT = "dd-M-yy";
var JQ_DATETIME_FORMAT = "dd-M-yy";

function GetIconCssClass(icon) {
    icon = icon.toLowerCase();
    switch (icon) {
        case "dashboard":
            return "fa fa-dashboard fa-fw";
        case "reports":
            return "fa fa-bar-chart-o fa-fw";
        case "sales":
            return "fa fa-bar-chart-o fa-fw";
        case "system":
            return "fa fa-cog fa-fw";
        case "product":
            return "fa fa-bar-chart-o fa-fw";
        default:
            return "";
    }
}
function logout() {
    sessionStorage.removeItem("menu");
    //localStorage.clear();
}

function ShowLoader() {
    $("div#divLoading").addClass('show');
}
function HideLoader() {
    $("div#divLoading").removeClass('show');
}

function ServiceError(error) {
    if (error.status == 401) {
        ShowErrorMessage("Unauthorized access")
    } else {
        ShowErrorMessage("Some error occured, Please try again")
    }
}

function ServiceSuccessMessage(data) {
    data = JSON.parse(data);
    if (data.Type == 1) {
        ShowSucessMessage(data.Message);
    } else {
        ShowErrorMessage(data.Message);
    }
}


function SmoothScroll(toElementId, delay) {
    delay = (delay == null) ? 2000 : delay;
    $('html, body').animate({
        scrollTop: $("[id*=" + toElementId + "]").offset().top
    }, delay);
}


function isMobile() {
    if (navigator.userAgent.match(/Android/i) ||
			navigator.userAgent.match(/webOS/i) ||
			navigator.userAgent.match(/iPad/i) ||
			navigator.userAgent.match(/iPhone/i) ||
			navigator.userAgent.match(/iPod/i)
			) {
        return true;
    }
}

/*****************Cookie****************/
function setCookie(c_name, value, exdays) {
    try {
        if (!c_name) return false;
        var exdate = new Date();
        exdate.setDate(exdate.getDate() + exdays);
        var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
        document.cookie = c_name + "=" + c_value;
    }
    catch (err) {
        return false;
    };
    return true;
}
function getCookie(c_name) {
    try {
        var i, x, y,
                        ARRcookies = document.cookie.split(";");
        for (i = 0; i < ARRcookies.length; i++) {
            x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
            y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
            x = x.replace(/^\s+|\s+$/g, "");
            if (x == c_name) return unescape(y);
        };
    }
    catch (err) {
        return false;
    };
    return false;
}


/*******************Number Function************************/
function roundNumber(num, dec) {
    var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
    return result;
}


function PutComma(number) {
    var d = number.indexOf('.');
    var s2 = d === -1 ? number : number.slice(0, d);
    for (var i = s2.length - 3; i > 0; i -= 3)
        s2 = s2.slice(0, i) + ',' + s2.slice(i);
    if (d !== -1)
        s2 += number.slice(d);
    return s2;
}

/***************Utitlity Function*******************/
function ExcelColumnName(number) {
    var ordA = 'a'.charCodeAt(0);
    var ordZ = 'z'.charCodeAt(0);
    var len = ordZ - ordA + 1;

    var s = "";
    while (number >= 0) {
        s = String.fromCharCode(number % len + ordA) + s;
        number = Math.floor(number / len) - 1;
    }
    return s.toUpperCase();
}
