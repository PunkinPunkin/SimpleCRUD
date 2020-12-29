namespace Shared
{
    /// <summary>
    /// 常使用的Reason Code
    /// </summary>
    public enum CommonCode
    {
        /// <summary>
        /// 平台未授權
        /// </summary>
        [StringValue("平台未授權")]
        NoAuthorityPlatform = -90,
        /// <summary>
        /// Token已申請(尚未逾時)
        /// </summary>
        [StringValue("Token已申請(尚未逾時)")]
        AlreadyHaveToken = -12,
        /// <summary>
        /// Token逾時
        /// </summary>
        [StringValue("Token逾時")]
        TokenExpired = -11,
        /// <summary>
        /// 無效Token
        /// </summary>
        [StringValue("無效Token")]
        InvalidToken = -10,
        /// <summary>
        /// 帳號已申請
        /// </summary>
        [StringValue("帳號已申請")]
        AccountExist = -3,
        /// <summary>
        /// 密碼不符合規則
        /// </summary>
        [StringValue("密碼不符合規則")]
        PwdStrength = -2,
        /// <summary>
        /// 帳號或密碼錯誤
        /// </summary>
        [StringValue("帳號或密碼錯誤")]
        AccOrPwdIncorrect = -1,
        /// <summary>
        /// 請求成功
        /// </summary>
        [StringValue("請求成功")]
        OK = 0,
        /// <summary>
        /// 帳號不存在
        /// </summary>
        [StringValue("帳號不存在")]
        AccountNotExist = 1,
        /// <summary>
        /// 密碼不正確
        /// </summary>
        [StringValue("密碼不正確")]
        PasswordError = 2,
        /// <summary>
        /// 沒有 {0} 權限
        /// </summary>
        [StringValue("沒有 {0} 權限")]
        NoAuthority = 3,
        /// <summary>
        /// {0} 不可為空
        /// </summary>
        [StringValue("{0} 不可為空")]
        Empty = 4,
        /// <summary>
        /// 查詢不到 {0} 資料
        /// </summary>
        [StringValue("查詢不到 {0} 資料")]
        NoData = 5,
        /// <summary>
        /// 新增 {0} 資料失敗
        /// </summary>
        [StringValue("新增 {0} 資料失敗")]
        InsertDataFail = 6,
        /// <summary>
        /// 修改 {0} 資料失敗
        /// </summary>
        [StringValue("修改 {0} 資料失敗")]
        UpdateDataFail = 7,
        /// <summary>
        /// 刪除 {0} 資料失敗
        /// </summary>
        [StringValue("刪除 {0} 資料失敗")]
        DeleteDataFail = 8,
        /// <summary>
        /// {0} 資料未維護完全
        /// </summary>
        [StringValue("{0} 資料未維護完全")]
        DataLoss = 9,
        /// <summary>
        /// 檢查錯誤
        /// </summary>
        [StringValue("檢查錯誤")]
        CheckError = 10,
        /// <summary>
        /// 沒有要變更資料
        /// </summary>
        [StringValue("沒有要變更資料")]
        NotChangeData = 11,
        /// <summary>
        /// {0} 必須為空
        /// </summary>
        [StringValue("{0} 必須為空")]
        NeedEmpty = 12,
        /// <summary>
        /// {0} 為不支援動作
        /// </summary>
        [StringValue("{0} 為不支援動作")]
        NotSupport = 13,
        /// <summary>
        /// {0} 序號被鎖定
        /// </summary>
        [StringValue("{0} 序號被鎖定")]
        SNHold = 14,
        /// <summary>
        /// 傳入參數錯誤
        /// </summary>
        [StringValue("傳入參數錯誤")]
        ParameterError = 15,
        /// <summary>
        /// 檔案不存在 {0}
        /// </summary>
        [StringValue("檔案不存在 {0}")]
        FileNotExist = 16,
        /// <summary>
        /// 應用程式已更版，請重新啓動。
        /// </summary>
        [StringValue("應用程式已更版，請重新啓動")]
        AppVersionUpdate = 17,
        /// <summary>
        /// {0} 資料庫程序 {1} 執行錯誤。
        /// </summary>
        [StringValue("{0} 資料庫程序 {1} 執行錯誤")]
        RunProcedureError = 18,
        /// <summary>
        /// 伺服機內容錯誤 : {0}
        /// </summary>
        [StringValue("伺服機內容錯誤 : {0}")]
        Fail = 99999,
    }
}
