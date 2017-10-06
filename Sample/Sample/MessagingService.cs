using System;
using System.Collections.Generic;
using Plugin.Messaging;

namespace Sample {
	public static class MessagingService {
		public static void SendSMS(string number, string message = "") {
			if (string.IsNullOrWhiteSpace(number)) {
				Helper.Log("Phone number cannot be left empty", true);
				return;
			}
			if (CrossMessaging.Current.SmsMessenger.CanSendSms) {
				CrossMessaging.Current.SmsMessenger.SendSms(number, message);
			} else
				Helper.Log("Cannot send SMS", true);
		}

		public static void CallTo(string number) {
			if (string.IsNullOrWhiteSpace(number)) {
				Helper.Log("Phone number cannot be left empty", true);
				return;
			}
			if (CrossMessaging.Current.PhoneDialer.CanMakePhoneCall) {
				CrossMessaging.Current.PhoneDialer.MakePhoneCall(number);
			} else
				Helper.Log("Cannot make Phone Call", true);
		}

		public static void SendEmail(string mailTo) {
			MailTo(mailTo, null, false, null, null, null, null);
		}

		public static void SendEmailWithBody(string mailTo, string subject, bool isHTML, string body) {
			if (string.IsNullOrWhiteSpace(subject)) {
				Helper.Log("Subject cannot be left empty.", true);
				return;
			}
			if (string.IsNullOrWhiteSpace(body)) {
				Helper.Log("Body cannot be left empty.", true);
				return;
			}
			MailTo(mailTo, subject, isHTML, body, null, null, null);
		}

		public static void SendEmailWithBodyAndCC(string mailTo, string subject, bool isHTML, string body, List<string> CCs) {
			if (string.IsNullOrWhiteSpace(subject)) {
				Helper.Log("Subject cannot be left empty.", true);
				return;
			}
			if (string.IsNullOrWhiteSpace(body)) {
				Helper.Log("Body cannot be left empty.", true);
				return;
			}
			if (CCs == null || CCs.Count <= 0) {
				Helper.Log("CCs cannot be left empty.", true);
				return;
			}
			MailTo(mailTo, subject, isHTML, body, CCs, null, null);
		}

		private static void MailTo(string mailTo, string subject, bool isHTML, string body, List<string> CCs, List<string> BCCs, List<EmailAttachmentModel> attachments) {
			if (string.IsNullOrWhiteSpace(mailTo)) {
				Helper.Log("MailTo cannot be left empty.", true);
				return;
			}

			if (CrossMessaging.Current.EmailMessenger.CanSendEmail) {
				var mail = new EmailMessageBuilder().To(mailTo);

				// add subject
				if (!string.IsNullOrWhiteSpace(subject)) {
					mail = mail.Subject(subject);
				}

				// add body
				if (!string.IsNullOrWhiteSpace(body)) {
					if (isHTML) {
						mail = mail.BodyAsHtml(body);
					} else {
						mail = mail.Body(body);
					}
				}

				// add CC
				if (CCs != null && CCs.Count > 0) {
					mail = mail.Cc(CCs);
				}

				// add BCCs
				if (BCCs != null && BCCs.Count > 0) {
					mail = mail.Bcc(BCCs);
				}

				// add attachments
				if (attachments != null && attachments.Count > 0) {
					foreach (EmailAttachmentModel val in attachments) {
						mail = mail.WithAttachment(val.FileName, val.FileType);
					}
				}

				try {
					CrossMessaging.Current.EmailMessenger.SendEmail(mail.Build());
				} catch (Exception ex) {
					Helper.Log(ex);
				}
			} else {
				Helper.Log("Cannot send email", true);
			}
		}
	}

	public class EmailAttachmentModel {
		private string _fileName = "";
		public string FileName {
			get { return _fileName; }
			set {
				if (string.IsNullOrWhiteSpace(value)) {
					throw new Exception("FileName cannot be null or empty.");
				}
				_fileName = value;
			}
		}

		private string _fileType = "";
		public string FileType {
			get { return _fileType; }
			set {
				if (string.IsNullOrWhiteSpace(value)) {
					throw new Exception("FileType cannot be null or empty.");
				}
				_fileType = value;
			}
		}
	}
}
