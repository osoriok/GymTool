#pragma checksum "C:\Users\Usuario\source\repos\GymTool\GymTool\Areas\Users\Pages\Account\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4e732e12ec9af21c4a79cefbcc486c767e99274f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(GymTool.Areas.Users.Pages.Account.Account.Areas_Users_Pages_Account_Details), @"mvc.1.0.razor-page", @"/Areas/Users/Pages/Account/Details.cshtml")]
namespace GymTool.Areas.Users.Pages.Account.Account
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Usuario\source\repos\GymTool\GymTool\Areas\Users\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Usuario\source\repos\GymTool\GymTool\Areas\Users\Pages\_ViewImports.cshtml"
using GymTool.Areas.Users.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Usuario\source\repos\GymTool\GymTool\Areas\Users\Pages\_ViewImports.cshtml"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemMetadataAttribute("RouteTemplate", "/Personal/Informacion")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4e732e12ec9af21c4a79cefbcc486c767e99274f", @"/Areas/Users/Pages/Account/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"28331733d73c9600f2188c1f11dedb97530b6a07", @"/Areas/Users/Pages/_ViewImports.cshtml")]
    public class Areas_Users_Pages_Account_Details : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("Atr&aacute;s"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary text-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Users", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "Users", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "Register", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Usuario\source\repos\GymTool\GymTool\Areas\Users\Pages\Account\Details.cshtml"
  
    if (Model.Input.DataUser != null)
    {
        var name = Model.Input.DataUser.Nombre + " " + Model.Input.DataUser.Apellidos;


#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"container p-4\" style=\"min-width: 320px\">\r\n\r\n            <h1>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4e732e12ec9af21c4a79cefbcc486c767e99274f6168", async() => {
                WriteLiteral("Atr&aacute;s");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                ");
#nullable restore
#line 12 "C:\Users\Usuario\source\repos\GymTool\GymTool\Areas\Users\Pages\Account\Details.cshtml"
           Write(name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </h1>
            <div class=""row"">


                <div class=""col"" style=""min-width: 300px"">
                    <div class=""card"">
                        <div class=""card-body"">
                            <h2>Informaci&oacute;n</h2>


                            <table class=""tableCursos"">
                                <tbody>
                                    <tr>
                                        <th>
                                            Identificaci&oacute;n
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>");
#nullable restore
#line 32 "C:\Users\Usuario\source\repos\GymTool\GymTool\Areas\Users\Pages\Account\Details.cshtml"
                                          Write(Model.Input.DataUser.Cedula);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            Nombre
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>");
#nullable restore
#line 42 "C:\Users\Usuario\source\repos\GymTool\GymTool\Areas\Users\Pages\Account\Details.cshtml"
                                          Write(name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            C&oacute;digo
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>");
#nullable restore
#line 52 "C:\Users\Usuario\source\repos\GymTool\GymTool\Areas\Users\Pages\Account\Details.cshtml"
                                          Write(Model.Input.DataUser.Codigo);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            Tel&eacute;fonos de contacto
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>");
#nullable restore
#line 62 "C:\Users\Usuario\source\repos\GymTool\GymTool\Areas\Users\Pages\Account\Details.cshtml"
                                          Write(Model.Input.DataUser.Telefono);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                            <p>");
#nullable restore
#line 63 "C:\Users\Usuario\source\repos\GymTool\GymTool\Areas\Users\Pages\Account\Details.cshtml"
                                          Write(Model.Input.DataUser.TelefonoEmergencia);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            Correo electronico
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>");
#nullable restore
#line 73 "C:\Users\Usuario\source\repos\GymTool\GymTool\Areas\Users\Pages\Account\Details.cshtml"
                                          Write(Model.Input.DataUser.Correo);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class=""col text-right"">

                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4e732e12ec9af21c4a79cefbcc486c767e99274f12550", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 86 "C:\Users\Usuario\source\repos\GymTool\GymTool\Areas\Users\Pages\Account\Details.cshtml"
                                      
                                        var dataUser = JsonConvert.SerializeObject(Model.Input.DataUser);
                                    

#line default
#line hidden
#nullable disable
                WriteLiteral("                                    <input type=\"hidden\" name=\"accUsuario\" id=\"accUsuario\" value=\"false\" />\r\n                                    <input type=\"hidden\" name=\"DataUser\"");
                BeginWriteAttribute("value", " value=\"", 4126, "\"", 4143, 1);
#nullable restore
#line 90 "C:\Users\Usuario\source\repos\GymTool\GymTool\Areas\Users\Pages\Account\Details.cshtml"
WriteAttributeValue("", 4134, dataUser, 4134, 9, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
                                    <input type=""submit"" name=""accion"" title=""Eliminar""  value=""Eliminar"" class=""btn btn-danger text-white"" onclick=""javascript: confirmar('Eliminar','Usuario')"">
                                    <input type=""submit"" name=""accion"" title=""Editar""  value=""  Editar  "" class=""btn btn-success "">
                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Area = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Page = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n\r\n            </div>\r\n\r\n\r\n        </div>\r\n");
#nullable restore
#line 103 "C:\Users\Usuario\source\repos\GymTool\GymTool\Areas\Users\Pages\Account\Details.cshtml"
    }

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DetailsModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<DetailsModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<DetailsModel>)PageContext?.ViewData;
        public DetailsModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
