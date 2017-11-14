
// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "SG0016:Controller method is vulnerable to CSRF", Justification = "<暫止>")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "SG0018:Path traversal", Justification = "<暫止>", Scope = "member", Target = "~M:IdentitySample.Controllers.SmtpConfigureOptions.PostConfigure(System.String,System.Net.Mail.SmtpClient)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Maintainability", "RCS1168:Parameter name differs from base name.", Justification = "<暫止>", Scope = "member", Target = "~M:IdentitySample.Controllers.EmailService.SendAsync(System.Action{System.Net.Mail.MailMessage})~System.Threading.Tasks.Task")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Maintainability", "RCS1168:Parameter name differs from base name.", Justification = "<暫止>", Scope = "member", Target = "~M:IdentitySample.Controllers.SmtpConfigureOptions.PostConfigure(System.String,System.Net.Mail.SmtpClient)")]

