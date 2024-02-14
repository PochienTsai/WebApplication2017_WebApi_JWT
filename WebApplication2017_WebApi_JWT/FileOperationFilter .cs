using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace WebApplication2017_WebApi_JWT
{
    public class FileOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.operationId.ToLower().Contains("upload"))
            {
                if (operation.parameters == null)
                {
                    operation.parameters = new List<Parameter>();
                }
                else
                {
                    operation.parameters.Clear();
                }

                operation.parameters.Add(new Parameter
                {
                    name = "File1",
                    @in = "formData",
                    description = "Upload software package",
                    required = false,
                    type = "file"
                });
                operation.parameters.Add(new Parameter
                {
                    name = "File2",
                    @in = "formData",
                    description = "Upload software package",
                    required = false,
                    type = "file"
                });
                operation.description = "swagger upload";

                operation.consumes.Add("application/form-data");
            }
        }
    }
}