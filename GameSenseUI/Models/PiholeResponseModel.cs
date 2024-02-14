using System.Text.Json.Serialization;
using GameSenseUI.Models.Gravity;

namespace GameSenseUI.Models;

public class PiholeResponseModel
{
	[JsonPropertyName("domains_being_blocked")]
	public string? DomainsBeingBlocked { get; set; }

	[JsonPropertyName("dns_queries_today")]
	public string? DnsQueriesToday { get; set; }

	[JsonPropertyName("ads_blocked_today")]
	public string? AdsBlockedToday { get; set; }

	[JsonPropertyName("ads_percentage_today")]
	public string? AdsPercentageToday { get; set; }

	[JsonPropertyName("unique_domains")] public string? UniqueDomains { get; set; }

	[JsonPropertyName("queries_forwarded")]
	public string? QueriesForwarded { get; set; }

	[JsonPropertyName("queries_cached")] public string? QueriesCached { get; set; }

	[JsonPropertyName("clients_ever_seen")]
	public string? ClientsEverSeen { get; set; }

	[JsonPropertyName("unique_clients")] public string? UniqueClients { get; set; }

	[JsonPropertyName("dns_queries_all_types")]
	public string? DnsQueriesAllTypes { get; set; }

	[JsonPropertyName("reply_UNKNOWN")] public string? ReplyUnknown { get; set; }

	[JsonPropertyName("reply_NODATA")] public string? ReplyNoData { get; set; }

	[JsonPropertyName("reply_NXDOMAIN")] public string? ReplyNxDomain { get; set; }

	[JsonPropertyName("reply_CNAME")] public string? ReplyCname { get; set; }

	[JsonPropertyName("reply_IP")] public string? ReplyIp { get; set; }

	[JsonPropertyName("reply_DOMAIN")] public string? ReplyDomain { get; set; }

	[JsonPropertyName("reply_RRNAME")] public string? ReplyRrName { get; set; }

	[JsonPropertyName("reply_SERVFAIL")] public string? ReplyServFail { get; set; }

	[JsonPropertyName("reply_REFUSED")] public string? ReplyRefused { get; set; }

	[JsonPropertyName("reply_NOTIMP")] public string? ReplyNotImp { get; set; }

	[JsonPropertyName("reply_OTHER")] public string? ReplyOther { get; set; }

	[JsonPropertyName("reply_DNSSEC")] public string? ReplyDnsSec { get; set; }

	[JsonPropertyName("reply_NONE")] public string? ReplyNone { get; set; }

	[JsonPropertyName("reply_BLOB")] public string? ReplyBlob { get; set; }

	[JsonPropertyName("dns_queries_all_replies")]
	public string? DnsQueriesAllReplies { get; set; }

	[JsonPropertyName("privacy_level")] public string? PrivacyLevel { get; set; }

	[JsonPropertyName("status")] public string? Status { get; set; }

	[JsonPropertyName("gravity_last_updated")]
	public LastUpdatedModel? GravityLastUpdated { get; set; }
}