namespace gsDesign.Explorer.ViewModels.Test
{
	using System;
	using System.Text;
	using Subfuzion.R.Rserve.Protocol;

	public class DiagnosticInfo
	{
		public DiagnosticInfo(Response response)
		{
			Response = response;
		}

		internal Response Response { get; set; }

		public override string ToString()
		{
			var sb = new StringBuilder();

			try
			{
				sb.Append("Response\n")
					.Append("========\n")
					.Append("For command: ").Append(Response.Request.CommandCode).Append('\n')
					.Append("IsOK: ").Append(Response.IsOk).Append('\n')
					.Append("IsError: ").Append(Response.IsError).Append('\n')
					.Append("ErrorCode: ").Append(Response.ErrorCode.ToString()).Append('\n')
					.Append('\n')
					;

				sb.Append("Payload\n")
					.Append("========\n")
					.Append("PayloadCode: ").Append(Response.Payload.PayloadCode).Append('\n')
					.Append("PayloadSize: ").Append(Response.Payload.PayloadSize).Append('\n')
					.Append("PayloadContentSize: ").Append(Response.Payload.ContentSize).Append('\n')
					.Append('\n')
					;

				var payload = Response.Payload;
				sb.Append("Payload content\n")
					.Append("===============\n")
					.Append("ContentSize: ").Append(payload.ContentSize).Append('\n')
					.Append('\n')
					;

				var data = payload.Content;
				for (int i = 0; i < payload.ContentSize; i++)
				{
					if (i > 0)
					{
						if (i % 8 == 0) sb.Append('\n');
						else if (i % 4 == 0) sb.Append(" ");
					}

					sb.Append(data[i].ToString("X4")).Append(" ");
				}
			}
			catch (Exception e)
			{
				sb.Append("\n(TODO) unsupported response, need to handle this!\n").Append(e);
			}

			return sb.ToString();
		}
	}
}
