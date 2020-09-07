using System;
using System.Collections;
using System.Data;
using OutSystems.HubEdition.RuntimePlatform;

namespace OutSystems.NssJWT_Extension {

	public interface IssJWT_Extension {

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ssPaymentId"></param>
		/// <param name="ssOrderId"></param>
		/// <param name="ssSecret"></param>
		/// <param name="ssSignedToken"></param>
		void MssCreateToken(string ssPaymentId, string ssOrderId, string ssSecret, out string ssSignedToken);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ssJWT_Token"></param>
		/// <param name="ssPaymentId"></param>
		/// <param name="ssOrderId"></param>
		void MssReadToken(string ssJWT_Token, out string ssPaymentId, out string ssOrderId);

	} // IssJWT_Extension

} // OutSystems.NssJWT_Extension
