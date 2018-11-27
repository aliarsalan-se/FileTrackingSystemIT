using System.Web.Mvc;
using DevExpress.Utils;
using DevExpress.Web.Mvc;

namespace DevExpress.Web.Demos {
    public class GridViewFeaturesHelper {
        public static void SetupGlobalGridViewBehavior(GridViewSettings settings) {
            settings.EnablePagingGestures = AutoBoolean.False;
            settings.SettingsPager.EnableAdaptivity = true;
            settings.Styles.Header.Wrap = DefaultBoolean.True;
            settings.Styles.GroupPanel.CssClass = "GridNoWrapGroupPanel";
        }
        public static MvcHtmlString GetHeadPartialResources() {
            return new MvcHtmlString(GetGridNoWrapGroupPanelCssStyle());
        }
        public static string GetGridNoWrapGroupPanelCssStyle() {
            return "\r\n<style>.GridNoWrapGroupPanel td.dx-wrap { white-space: nowrap !important; }</style>\r\n";
        }
    }
}
