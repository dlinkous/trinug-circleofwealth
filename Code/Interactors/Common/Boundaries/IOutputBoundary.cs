using System;

namespace CircleOfWealth.Interactors.Common.Boundaries
{
	public interface IOutputBoundary<TResponseModel>
	{
		void HandleResponse(TResponseModel responseModel);
	}
}
