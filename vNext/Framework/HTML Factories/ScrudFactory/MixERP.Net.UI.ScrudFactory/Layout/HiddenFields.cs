using System.Globalization;
using System.Text;

namespace MixERP.Net.UI.ScrudFactory.Layout
{
    internal sealed class HiddenFields : ScrudLayout
    {
        internal HiddenFields(Config config)
            : base(config)
        {
        }

        public override string Get()
        {
            StringBuilder hiddenFields = new StringBuilder();
            hiddenFields.Append(GetHiddenField("UserIdHidden", this.Config.UserId));
            hiddenFields.Append(GetHiddenField("UsernameHidden", this.Config.UserName));
            hiddenFields.Append(GetHiddenField("OfficeIdHidden", this.Config.OfficeId));
            hiddenFields.Append(GetHiddenField("OfficeCodeHidden", this.Config.OfficeCode));
            hiddenFields.Append(GetHiddenField("LastValueHidden", string.Empty));
            
            return hiddenFields.ToString();
        }

        private static string GetHiddenField(string id, object value)
        {
            string hiddenField = "<input type='hidden' id='{0}' value='{1}' />";

            hiddenField = string.Format(CultureInfo.InvariantCulture, hiddenField, id, value);

            return hiddenField;
        }
    }
}