<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Prototype.aspx.cs" Inherits="MixERP.Net.Utility.LocalizationManager.Web.Prototype" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.9.1.js"></script>
    <link href="Scripts/semantic-ui/semantic.min.css" rel="stylesheet" />
    <script src="Scripts/semantic-ui/semantic.js"></script>
    <style type="text/css">
        .hidden {
            display: none;
        }

        html, body {
            font-family: 'Segoe UI';
        }

        .sixteen.wide.column {
            background-color: #FAFAFA;
            border: 1px solid #F0F0F0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ui page grid">
            <div class="sixteen wide column">
                <h1>Welcome Binod :)</h1>
                <div class="ui divider"></div>
                <h2 class="ui blue header">English (US): The task was completed successfully.</h2>
                <h4 class="ui pink header">Which one is correct in French (France)?</h4>

                <div class="ui form">
                    <div class="ui items">
                        <div class="item">
                            <div class="ui star rating" data-rating="0"></div>
                            La tâche a été achevée avec succès.
                        </div>
                        <div class="item">
                            <div class="ui star rating" data-rating="0"></div>
                            La tâche a été un succès
                        </div>
                    </div>

                    <div class="answer field">
                        <label>Your Answer (Optional)</label>
                        <div class="ui huge input">
                            <input type="text">
                        </div>
                    </div>

                    <button class="ui green button" data-content="Your IP Address is being logged for recording misuse.">Submit</button>
                    <div class="ui orange button">Skip This</div>
                    <div class="ui red button">I'm Done</div>
                </div>
            </div>

        </div>

        <script type="text/javascript">
            var checkboxes = $('.ui.checkbox');
            checkboxes.checkbox();

            checkboxes.find("input").change(function () {
                var control = $(this);
                var answer = $(".answer");

                if (control.attr("id") === "None" && control.is(":checked")) {
                    answer.show(500);
                    answer.find("input").focus();
                    return;
                };

                answer.hide(1000);
            });


            $('.ui.rating').rating({ maxRating: 5 });

            $('.activating.element').popup();

            $('.button').popup({inline: true});
        </script>
    </form>
</body>
</html>
