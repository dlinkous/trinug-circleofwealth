using System;
using CircleOfWealth.Interactors.Common.Boundaries;

namespace CircleOfWealth.Interactors.UnitTests.Presenters
{
	internal class Presenter<TResponseModel> : IOutputBoundary<TResponseModel>
	{
		internal TResponseModel ResponseModel;

		public void HandleResponse(TResponseModel responseModel) => ResponseModel = responseModel;
	}
}
