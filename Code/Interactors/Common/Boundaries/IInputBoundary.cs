using System;

namespace CircleOfWealth.Interactors.Common.Boundaries
{
	public interface IInputBoundary<TRequestModel, TResponseModel>
	{
		void HandleRequest(TRequestModel requestModel, IOutputBoundary<TResponseModel> outputBoundary);
	}
}
