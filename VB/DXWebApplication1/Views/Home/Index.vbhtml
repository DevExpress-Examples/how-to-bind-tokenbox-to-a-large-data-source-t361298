
<script type="text/javascript">
    var minFilterLength = 3;
    var dataRequestTimer;
    var dataRequestDelay = 500;

    function keyUp(s, e) {
        var forbiddenCodes = [getCode("DOWN"), getCode("UP"), getCode("LEFT"), getCode("RIGHT"), getCode("HOME"), getCode("END")];
        if (forbiddenCodes.indexOf(e.htmlEvent.keyCode) > -1) return;
        clearTimeout(dataRequestTimer);
        clearItems();
        var inputValue = s.GetInputElement().value;
        if (inputValue.length >= minFilterLength) {
            dataRequestTimer = setTimeout(function () {
                var data = { 'str': inputValue, 'seltokens': token.GetValue() };
                $.ajax({
                    type: "POST",
                    url: '@Url.Action("GetFilteredData", "Home")',
                    data: JSON.stringify(data),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: setData
                });
            }, dataRequestDelay);
        }
    }
    function setData(val, status) {
        if (val === undefined || val === null) return;
        token.BeginUpdate();
        for (var i = 0, l = val.length; i < l; i++)
            token.AddItem(val[i].ProductName, val[i].ProductID);
        token.EndUpdate();
        token.ShowDropDown();
    }
    function tokensChanged(s, e) {
        clearItems();
        token.HideDropDown();
    }
    function clearItems() {
        token.BeginUpdate();
        for (var i = 0, l = token.GetItemCount() ; i < l; i++)
            token.RemoveItem(0);
        token.EndUpdate();
    }
    function getCode(name) { return ASPxClientUtils.StringToShortcutCode(name); }
</script>


Important note: after a search string will be inputed
only 10 items from the whole list of item match this filter creteria
<br />
<br />
Start type Cars, John, Jane, Michael, Lana and you will see how it works
<br />
<br />

@Html.DevExpress().TokenBox(Sub(settings)
                                     settings.Name = "token"
                                     settings.Properties.ValueField = "ProductID"
                                     settings.Properties.TextField = "ProductName"
                                     settings.Properties.ItemValueType = GetType(System.String)
                                     settings.Properties.AllowCustomTokens = True
                                     settings.Properties.ClientSideEvents.KeyUp = "keyUp"
                                     settings.Properties.ClientSideEvents.TokensChanged = "tokensChanged"
                                     settings.Width = Unit.Pixel(500)

                                 End Sub).GetHtml()                                 