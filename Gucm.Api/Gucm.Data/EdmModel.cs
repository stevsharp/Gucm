using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gucm.Data
{
    public static class EdmModel
    {
        public static IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);

            builder.EnableLowerCamelCase();

            return builder.GetEdmModel();
        }
    }
}
