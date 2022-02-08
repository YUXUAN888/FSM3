//如果好用，请收藏地址，帮忙分享。
using System.Collections.Generic;
public class DD
{
    /// <summary>
    /// 
    /// </summary>
    public string avatar_url { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string nick_name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string content { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string is_todo { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string is_admin { get; set; }
}
public class Pagination
{
    /// <summary>
    /// 
    /// </summary>
    public int per_page { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int current_page { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string next_page_url { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string prev_page_url { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string from { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string to { get; set; }
}

public class Root
{
    /// <summary>
    /// 
    /// </summary>
    public int status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string message { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Pagination pagination { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Object { get; set; }
/// <summary>
/// 
/// </summary>
public List<DD> data { get; set; }
}