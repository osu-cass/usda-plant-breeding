
var errorFocus = 0;
var blurEvent = 0;
var elemId;
function getDataList(textId) {
    return $(textId).parent().find("[name='listid9112']");
}
function getHiddenId(textId) {
    return $(textId).parent().find("#hiddenid9112");
}

function findTermAfterBlur(textId) {
    clearNotyQueue();
    var datalist = getDataList(textId);
    var hiddenId = getHiddenId(textId);
    var url = $(textId).attr('data-url');
    var genusId = $(textId).attr('data-genus');
    var term = $(textId).val().trim();
    blurEvent++;
    if (datalist.children().length > 0) {
        matchTerm(term, datalist, hiddenId, textId, false);
        hiddenId = getHiddenId(textId);
    }

    //if the id is not set and there is something in the search box.
    if ((hiddenId.val().length == 0 || hiddenId.val() == 0 || datalist.children().length == 0) && term.length > 0) {
        $.when(getAjax(term, datalist, textId, url, genusId, hiddenId)).done(function (data) {
                     datalist = getDataList(textId);
                     hiddenId = getHiddenId(textId);

                     matchTerm(term, datalist, hiddenId, textId, true);
                     HighlightFocus(term, textId, true);
                });
    }
   
    if (term.length == 0 || $(getHiddenId(textId)).val().length != 0) {
        $(textId).css("border-style", "").css("border-color", "");
        errorFocus = 0;

    }

}

function HighlightFocus(term, textId, animate) {
    var hiddenId = getHiddenId(textId);
    if (term.length == 0) {
        clearFormInput(hiddenId, textId);
    }

    if (animate && term.length != 0 && $(hiddenId).val().length == 0) {
        if (errorFocus == 0) {
            $(textId).animate({ backgroundColor: '#ff4d4d' }, 1000).animate({ backgroundColor: '' }, 1000);

            FocusOnItem($(textId));
            var tmp = $(textId).val();
            $(textId).val('');
            $(textId).val(tmp);
            errorFocus++;
        }
        $(textId).css("border-style", "solid").css("border-color", "#ff0000");
    }

    if (term.length == 0 || $(hiddenId).val().length != 0) {
        $(textId).css("border-style", "solid").css("border-color", "#ffffff");
        errorFocus = 0;
    }
    
    if (term.length == 0 || $(hiddenId).val().length != 0) {
        $(textId).css("border-style", "").css("border-color", "");
        errorFocus = 0;

    }

    if (animate && blurEvent > 1) {
        errorFocus = 0;
        blurEvent = 0;

    }

}

function findTerm(textId, event) {
        var key = event.keyCode || event.charCode;
        var datalist = getDataList(textId);
        var hiddenId = getHiddenId(textId);
        var term = $(textId).val().trim();
        var url = $(textId).attr('data-url');
        var genusId = $(textId).attr('data-genus');
        var timer = 400;
        var deleteTimer = 200;
        //if the datalist is empty, get some items
    if (datalist.children().length == 0) {
            $.when(getAjax(term, datalist, textId, url, genusId, hiddenId)).done(function (data) {
                if (getDataList(textId).children().length == 0) {
                            $(hiddenId).val('');
                        }
                        HighlightFocus(term, textId, false);
                    });

        }

        if (key != 8 && key != 46 && (key < 37 || key > 40)) {
            delay(getCallAjax(term, datalist, textId, url, genusId, hiddenId), timer);
        } else if (key == 8 || key == 46) {
            var term = $(textId).val().trim();
            if (term.length > 0) {
                $(hiddenId).val('');             
                delay(getCallAjax(term, datalist, textId, url, genusId, hiddenId), deleteTimer);

            } else {
                clearFormInput(hiddenId, textId);
            }
        }
        
        if (term.length == 0 || $(hiddenId).val().length != 0) {
            $(textId).css("border-style", "solid").css("border-color", "#ffffff");
        }
      
}

function setFormInputOption(id, value, hiddenId, textId) {
    var option = { id: id, value: value };
    delay(setFormInput(option, hiddenId, textId), 0);

}
function clearFormInput(hiddenId, textId) {
    setFormInputOption("", "", hiddenId, textId);

}
// set form input for given option
function setFormInput(option, hiddenId, textId) {
    if ((option.id && option.id.length > 0)|| $(hiddenId).val().length > 0) {
        $(hiddenId).val(option.id);
        $(hiddenId).change();

    }
    if ((option.value && option.value.length > 0) || $(textId).val().length > 0) {
        $(textId).val(option.value);
        $(textId).change();
    }

}


// match given term to option in data-list
function matchTerm(term, datalist, hiddenId, textId, notify) {
    // get options that exactly match
    var options = $(datalist).children('option');
    var matches = options.filter(function (index, element) {
        return $(this).attr('value').trim().toLowerCase() === term.trim().toLowerCase();
    });
    if (matches.length > 0) {
        // option exactly matches
        setFormInput(matches[0], hiddenId, textId);
    } else {
        // no option exactly matches; check if options represent one result
        var p = options.toArray().reduce(function (previousValue, currentValue, index, array) {
            return previousValue && currentValue.id == options[0].id;
        }, true);
        if (p) {
            // options represent one result; check if option partially matches
            matches = options.filter(function (index, element) {
                return $(this).attr('value').trim().toLowerCase().indexOf(term.trim()) > -1;
            });
            if (matches.length > 0) {
                // one option partially matches
                setFormInput(matches[0], hiddenId, textId);
            } else {
                // clear form
                $(hiddenId).val("").change();
            }
        } else {
            // clear form
            $(hiddenId).val("").change();
        }
    }

    if (notify && $(hiddenId).val().length == 0) {
        NotyMessage("Not Found", 1);
        $(textId).css("border-style", "solid").css("border-color", "#ff0000");


    }
}

// get search results for given term
function getCallAjax(term, dataList, textId, url, genusId, hiddenId) {
    return function () {
        getAjax(term, dataList, textId, url, genusId, hiddenId);
    };
}



// get search results for given term
function getAjax(term, dataList, textId, url, genusId, hiddenId) {
    return $.ajax({
            url: url,
            method: "get",
            dataType: "json",
            data: { 'term': term, 'genusId': genusId },

            success: function (data) {
                // clear data-list options
                $(dataList).empty();
                // add new options to data-list
                for (var i = 0; i < data.length; i++) {
                    var option = $(document.createElement('option'));
                    option.attr("id", data[i].key);
                    option.attr("value", data[i].value);
                    option.text(data[i].value);
                    $(dataList).append(option);
                }
                // check for non-standards compliant internet explorer
                if (navigator.userAgent.match(/Trident\/7\./)) {
                    $(textId).focus().click();
                }
             
            }
        });
}

// trigger given callback after given delay within closure that contains timeout id
var delay = (function () {
    var timer = 0;
    return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();

function clearNotyQueue() {

    $.noty.clearQueue();
}

function NotyMessage(message, maxVisible) {
    if (!maxVisible) {
        maxVisible = 2;
    }
    if (noty) {
        noty({
            text: message,
            layout: 'center',
            timeout: 4000,
            maxVisible: maxVisible,
            type: 'information',
            closeWith: ['click', 'hover']
        });
    }
}


function FocusOnItem(item) {
    setTimeout(function () {
        $(item).focus();
    }, 0);

}
function AutoCompleteObject(genusId, initVal, initKey, term, termId, nameObj, idObj, value) {
    this.GenusId = genusId;
    this.InitVal = initVal;
    this.InitKey = initKey;
    this.Term = term;
    this.TermId = termId;
    this.NameObj = nameObj,
    this.IdObj = idObj;
    this.Value = value;
}

function getAutoCompleteObject(nameElementId, idElementId, term, termId) {
    var nameObj = document.getElementById(nameElementId);
    var idObj = document.getElementById(idElementId);
    var genusId = nameObj.getAttribute('data-genus-id');
    var initVal = nameObj.getAttribute('data-init-val');
    var initKey = nameObj.getAttribute('data-init-key');
    return new AutoCompleteObject(genusId, initVal, initKey, term, termId, nameObj, idObj, nameObj.value);
}

function CreateAutoComplete(inputId, inputName, genusId, url) {


    var autocomplete = new autoComplete({
        selector: '#' +inputName,
        minChars: 1,
        source: function (term, suggest) {
            term = term.toLowerCase();
            var dataObj = { term: term }
            genusId = parseInt(genusId);
            if (!isNaN(genusId)) {
                dataObj.genusId = genusId;
            }
            console.log(dataObj);

            var choices = [];
            $.ajax({
                url: url,
                method: "get",
                dataType: "json",
                data: dataObj,

                success: function (data) {
                    for (var i = 0; i < data.length; i++) {
                        var choice = [data[i].key, data[i].value];
                        choices.push(choice);
                    }
                    var suggestions = [];
                    for (var i = 0; i < choices.length; i++) {
                        if (choices[i][1].substring(0, term.length).toLowerCase().trim() === term.toLowerCase().trim()) {

                            suggestions.push(choices[i]);
                        }
                        else {
                            console.log(term.toLowerCase().trim())
                        }
                    }
                    suggest(suggestions);
                }
            });

        },
        onSelect: function (e, term, item, termId) {
            var itemId = document.getElementById(inputId);
            itemId.value = termId;
        },
        onBlur: function (e, term, item, termId) {
            var autoComplete = getAutoCompleteObject(inputName, inputId, term, termId);
            setAutCompleteDomBlur(autoComplete);
        }
    });


}

function setAutCompleteDomBlur(autoComplete) {
    if (!(autoComplete instanceof AutoCompleteObject) || !autoComplete || !autoComplete.NameObj || !autoComplete.IdObj) {
        console.log("needs to be an autocomplete obj")
        return;
    }

    //Autocomplete has a text value but search returned nothing, do nothing
    if (autoComplete.Value && !autoComplete.TermId && !autoComplete.Term && autoComplete.Value.toLowerCase() == autoComplete.InitVal.trim().toLowerCase()) {
        return;
    }

    //if termId is blank 
    if (!autoComplete.TermId) {
        //if term is blank then we need to check the nameobj val. if nameobj val check init val and set id
        if ((autoComplete.Term || autoComplete.Term.trim() != '') && autoComplete.InitVal) {
            //if the old value is equal to the new val set the id to be init id
            if (autoComplete.InitVal.toLowerCase() == autoComplete.Term.trim().toLowerCase()) {
                autoComplete.IdObj.value = autoComplete.InitKey;
                $(autoComplete.IdObj).change();
            } else {
                autoComplete.IdObj.value = '';
                $(autoComplete.IdObj).change();

            }
        }else {
            autoComplete.IdObj.value = '';
            $(autoComplete.IdObj).change();

            //do focus here
            AutocompleteHighlight(autoComplete, false, true);

        }
    } else {
        autoComplete.IdObj.value = autoComplete.TermId;
        $(autoComplete.IdObj).change();
        AutocompleteHighlight(autoComplete, true, false);

    }

}


function AutocompleteHighlight(autoComplete, hasMatch, animate) {
    if (elemId != autoComplete.NameObj.id ) {
        errorFocus = 0;
        blurEvent = 0;
        elemId = autoComplete.NameObj.id;
    }

    if (!hasMatch ) {
        if (animate && errorFocus == 0) {
            $(autoComplete.NameObj).animate({ backgroundColor: '#ff4d4d' }, 1000).animate({ backgroundColor: '' }, 1000);
            FocusOnItem($(autoComplete.NameObj));
            NotyMessage("Invalid Search, Please Try Again", 1);
            errorFocus++;

        }
        $(autoComplete.NameObj).css("border-style", "solid").css("border-color", "#ff0000");
    }
    else {
        $(autoComplete.NameObj).css("border-style", "").css("border-color", "");
        errorFocus = 0;
        blurEvent = 0;
    }


}

