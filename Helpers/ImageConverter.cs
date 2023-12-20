namespace SimpleOLX.Helpers
{
	public static class ImageConverter
	{
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

		public static byte[] ConvertBase64StringToByteArray(string? base64String)
		{
			if (base64String == null) return new byte[] { };
			byte[] byteArray = Convert.FromBase64String(base64String);
			return byteArray;
		}

		public static string ConvertByteArrayToBase64String(byte[]? byteArray)
		{
			if (byteArray == null) return "";
			string base64String = Convert.ToBase64String(byteArray);
			return base64String;
		}
	}
}
