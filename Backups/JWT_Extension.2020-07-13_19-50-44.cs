using System;
using System.Collections;
using System.Data;
using OutSystems.HubEdition.RuntimePlatform;
using OutSystems.RuntimePublic.Db;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Security.Cryptography;

namespace OutSystems.NssJWT_Extension {

	public class CssJWT_Extension: IssJWT_Extension {

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ssPaymentId"></param>
		/// <param name="ssOrderId"></param>
		/// <param name="ssSecret"></param>
		/// <param name="ssSignedToken"></param>
		public void MssCreateToken(string ssPaymentId, string ssOrderId, string ssSecret, out string ssSignedToken)
		{
			// TODO: Write implementation for action

			//Header
			var header = "{\"alg\":\"HS256\",\"typ\":\"JWT\"}";
			var jsonHeader = JObject.Parse(header);
			var headerString = jsonHeader.ToString();
			var decodedHeaderBytes = Encoding.UTF8.GetBytes(headerString);
			var encodedHeader = Convert.ToBase64String(decodedHeaderBytes);

			//Payload
			DateTimeOffset initIAT = DateTimeOffset.UtcNow;
			long iat = initIAT.ToUnixTimeSeconds();
			DateTimeOffset initEXP = DateTimeOffset.UtcNow.AddHours(3);
			long exp = initEXP.ToUnixTimeSeconds();

			var payload = "{\"paymentDetails\":{\"paymentId\":\"\",\"orderId\":\"\",\"status\":\"SUCCESSFUL\"},\"exp\":1594657891,\"iat\":1516239022}";
			var jsonPayload = JObject.Parse(payload);
			var orderDetails = jsonPayload["paymentDetails"];
			orderDetails["paymentId"] = ssPaymentId;
			orderDetails["orderId"] = ssOrderId;
			jsonPayload["iat"] = iat;
			jsonPayload["exp"] = exp;

			var jsonString = jsonPayload.ToString();
			var decodedPayloadBytes = Encoding.UTF8.GetBytes(jsonString);
			var initEncodedPayload = Convert.ToBase64String(decodedPayloadBytes);
			var payloadLength = initEncodedPayload.Length;
			var encpayload = initEncodedPayload.Substring(0, payloadLength - 1);

			var token = encodedHeader + "." + encpayload;

			//Signature
			var signature = "";
			var encoding = new ASCIIEncoding();
			byte[] secretByte = encoding.GetBytes(ssSecret);
			byte[] tokenBytes = encoding.GetBytes(token);
			using (var hmacsha256 = new HMACSHA256(secretByte))
			{
				byte[] hashmessage = hmacsha256.ComputeHash(tokenBytes);
				signature = Convert.ToBase64String(hashmessage);
			}

			//SignedToken
			ssSignedToken = token + "." + signature;
		} // MssCreateToken

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ssJWT_Token"></param>
		/// <param name="ssPaymentId"></param>
		/// <param name="ssOrderId"></param>
		public void MssReadToken(string ssJWT_Token, out string ssPaymentId, out string ssOrderId) 
		{
			// TODO: Write implementation for action

			//Retrieving Payload from Token
			var data = ssJWT_Token.Substring(ssJWT_Token.IndexOf(".") + 1);
			var Maindata = data.Substring(0, data.IndexOf("."));

			if (Maindata.Length % 4 != 0)
				Maindata += new String('=', 4 - Maindata.Length % 4);

			byte[] base64EncodedBytes = Convert.FromBase64String(Maindata);
			var jsonData = Encoding.UTF8.GetString(base64EncodedBytes);

			//Retrieving details from Payload JSON
			var details = JObject.Parse(jsonData);
			var paymentId = details["paymentId"];
			var orderDetails = details["orderDetails"];
			var orderId = orderDetails["orderId"];

			ssPaymentId = paymentId.ToString();
			ssOrderId = orderId.ToString();
		} // MssReadToken


	} // CssJWT_Extension

} // OutSystems.NssJWT_Extension

