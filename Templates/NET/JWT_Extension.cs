using System;
using System.Collections;
using System.Data;
using OutSystems.HubEdition.RuntimePlatform;
using OutSystems.RuntimePublic.Db;

namespace OutSystems.NssJWT_Extension {

	public class CssJWT_Extension: IssJWT_Extension {

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ssPaymentId"></param>
		/// <param name="ssOrderId"></param>
		/// <param name="ssSecret"></param>
		/// <param name="ssSignedToken"></param>
		public void MssCreateToken(string ssPaymentId, string ssOrderId, string ssSecret, out string ssSignedToken) {
			ssSignedToken = "";
			// TODO: Write implementation for action
		} // MssCreateToken

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ssJWT_Token"></param>
		/// <param name="ssPaymentId"></param>
		/// <param name="ssOrderId"></param>
		public void MssReadToken(string ssJWT_Token, out string ssPaymentId, out string ssOrderId) {
			ssPaymentId = "";
			ssOrderId = "";
			// TODO: Write implementation for action
		} // MssReadToken

	} // CssJWT_Extension

} // OutSystems.NssJWT_Extension

