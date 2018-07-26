using System;
using CircleOfWealth.Interactors.Common.Boundaries;

namespace CircleOfWealth.ConsoleApplication
{
	internal class Presenter<TResponseModel> : IOutputBoundary<TResponseModel>
	{
		internal TResponseModel ResponseModel;

		public void HandleResponse(TResponseModel responseModel) => ResponseModel = responseModel;
	}
}
