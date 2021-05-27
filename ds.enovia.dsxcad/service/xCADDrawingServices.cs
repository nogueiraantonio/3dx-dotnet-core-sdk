//------------------------------------------------------------------------------------------------------------------------------------
// Copyright 2020 Dassault Systèmes - CPE EMED
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify,
// merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished
// to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS
// BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//------------------------------------------------------------------------------------------------------------------------------------

using ds.authentication;
using ds.enovia.dsxcad;
using ds.enovia.dsxcad.exception;
using ds.enovia.dsxcad.model;
using ds.enovia.service;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ds.enovia.dslc.changeaction.service
{
    public class xCADDrawingServices : EnoviaBaseService
    {        
        private const string BASE_RESOURCE = "/resources/v1/modeler/dsxcad/dsxcad:Drawing";
        private const string SEARCH = "/search";
      
        public string GetBaseResource()
        {
            return BASE_RESOURCE;
        }

        public xCADDrawingServices(string _enoviaService, IPassportAuthentication passport) : base(_enoviaService, passport)
        {

        }

        public async Task<xCADDrawing> GetXCADDrawing(string _id, bool _details = true)
        {
            string getXCADDrawingEndpoint = string.Format("{0}/{1}", GetBaseResource(), _id);

            Dictionary<string, string> queryParams = null;
            if (_details)
            {
                queryParams = new Dictionary<string, string>();
                queryParams.Add("$mask", "dsmvxcad:xCADDrawingMask.EnterpriseDetails");
            }

            HttpResponseMessage requestResponse = await GetAsync(getXCADDrawingEndpoint, queryParams);

            if (requestResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //handle according to established exception policy
                throw (new GetXCADDrawingException(requestResponse));
            }

            xCADDrawingSet xcadDWGSet = await requestResponse.Content.ReadFromJsonAsync<xCADDrawingSet>();
            
            if ((xcadDWGSet != null) && (xcadDWGSet.totalItems == 1))
            {
                return xcadDWGSet.member[0];
            }

            return null;
        }

        //Modifies the Drawing attributes
        public async Task<xCADDrawing> PatchXCADDrawingAttributes(string _id, xCADDrawingPatchAttributes _atts, bool _details = true)
        {
            string patchXCADDrawingEndpoint = string.Format("{0}/{1}", GetBaseResource(), _id);

            Dictionary<string, string> queryParams = null;
            if (_details)
            {
                queryParams = new Dictionary<string, string>();
                queryParams.Add("$mask", "dsmvxcad:xCADDrawingMask.EnterpriseDetails");
            }

            string bodyPatchMessage = _atts.toJson();
            
            HttpResponseMessage requestResponse = await PatchAsync(patchXCADDrawingEndpoint, queryParams, null, bodyPatchMessage);

            if (requestResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                //handle according to established exception policy
                throw (new GetXCADDrawingException(requestResponse));
            }

            xCADDrawingSet xcadDWGSet = await requestResponse.Content.ReadFromJsonAsync<xCADDrawingSet>();
            if ((xcadDWGSet != null) && (xcadDWGSet.totalItems == 1))
            {
                return xcadDWGSet.member[0];
            }

            return null;
        }
    }
}
