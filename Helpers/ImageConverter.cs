namespace SimpleOLX.Helpers
{
	/// <summary>
	/// Static class for converting images
	/// </summary>
	public static class ImageConverter
	{
        /// <summary>
        /// Convert IFormFile To Base64 String
        /// </summary>
        /// <param name="file">image file</param>
        /// <returns>string in base64 of that image file</returns>
        public static async Task<string> ConvertIFormFileToBase64String(IFormFile? file)
		{
			if (file == null) return "";
			using (MemoryStream stream = new MemoryStream())
			{
				await file.CopyToAsync(stream);
				byte[] imageBytes = stream.ToArray();
				string base64String = Convert.ToBase64String(imageBytes);
				return base64String;
			}
		}

        /// <summary>
        /// Convert IFormFile To Byte Array
        /// </summary>
        /// <param name="file">image file</param>
        /// <returns>byte array of image file</returns>
        public static async Task<byte[]> ConvertIFormFileToByteArray(IFormFile? file)
		{
			if (file == null) return new byte[] { };
			using (MemoryStream stream = new MemoryStream())
			{
				await file.CopyToAsync(stream);
				byte[] imageBytes = stream.ToArray();
				return imageBytes;
			}
		}

        /// <summary>
        /// Convert Base64 String To ByteArray
        /// </summary>
        /// <param name="base64String">base64 String</param>
        /// <returns>byte array of image in base64, or empty array if nothing to convert</returns>
        public static byte[] ConvertBase64StringToByteArray(string? base64String)
		{
			if (base64String == null) return new byte[] { };
			byte[] byteArray = Convert.FromBase64String(base64String);
			return byteArray;
		}

        /// <summary>
        /// Convert ByteArray To Base64String
        /// </summary>
        /// <param name="byteArray">byte Array</param>
        /// <returns>Base64 String, or empty string if nothing to convert</returns>
        public static string ConvertByteArrayToBase64String(byte[]? byteArray)
		{
			if (byteArray == null) return "";
			string base64String = Convert.ToBase64String(byteArray);
			return base64String;
		}
	}
}
