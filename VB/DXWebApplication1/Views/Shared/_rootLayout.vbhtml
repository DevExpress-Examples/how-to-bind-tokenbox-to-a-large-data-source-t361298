﻿<!DOCTYPE HTML>

<html>
<head>
    <title>@ViewBag.Title</title>
    @Html.DevExpress().GetStyleSheets(
                    New StyleSheet With {.ExtensionSuite = ExtensionSuite.Editors})
    
    @Html.DevExpress().GetScripts(
								New Script With {.ExtensionSuite = ExtensionSuite.Editors})
</head>

<body>
    @RenderBody()
</body>
</html>