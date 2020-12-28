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
        NoAuthorityPlatform = -90,
        /// <summary>
        /// Token已申請(尚未逾時)
        /// </summary>
        AlreadyHaveToken = -12,
        /// <summary>
        /// Token逾時
        /// </summary>
        TokenExpired = -11,
        /// <summary>
        /// 無效Token
        /// </summary>
        InvalidToken = -10,
        /// <summary>
        /// 帳號已申請
        /// </summary>
        AccountExist = -3,
        /// <summary>
        /// 密碼不符合規則
        /// </summary>
        PwdStrength = -2,
        /// <summary>
        /// 帳號或密碼錯誤
        /// </summary>
        AccOrPwdIncorrect = -1,
        /// <summary>
        /// 請求成功
        /// </summary>
        OK = 0,
        /// <summary>
        /// 帳號不存在
        /// </summary>
        AccountNotExist = 1,
        /// <summary>
        /// 密碼不正確
        /// </summary>
        PasswordError = 2,
        /// <summary>
        /// 沒有 {0} 權限
        /// </summary>
        NoAuthority = 3,
        /// <summary>
        /// {0} 不可為空
        /// </summary>
        Empty = 4,
        /// <summary>
        /// 查詢不到 {0} 資料
        /// </summary>
        NoData = 5,
        /// <summary>
        /// 新增 {0} 資料失敗
        /// </summary>
        InsertDataFail = 6,
        /// <summary>
        /// 修改 {0} 資料失敗
        /// </summary>
        UpdateDataFail = 7,
        /// <summary>
        /// 刪除 {0} 資料失敗
        /// </summary>
        DeleteDataFail = 8,
        /// <summary>
        /// {0} 資料未維護完全
        /// </summary>
        DataLoss = 9,
        /// <summary>
        /// 檢查錯誤
        /// </summary>
        CheckError = 10,
        /// <summary>
        /// 沒有要變更資料
        /// </summary>
        NotChangeData = 11,
        /// <summary>
        /// {0} 必須為空
        /// </summary>
        NeedEmpty = 12,
        /// <summary>
        /// {0} 為不支援動作
        /// </summary>
        NotSupport = 13,
        /// <summary>
        /// {0} 序號被鎖定
        /// </summary>
        SNHold = 14,
        /// <summary>
        /// 傳入參數錯誤
        /// </summary>
        ParameterError = 15,
        /// <summary>
        /// 檔案不存在 {0}
        /// </summary>
        FileNotExist = 16,
        /// <summary>
        /// 應用程式已更版，請重新啓動。
        /// </summary>
        AppVersionUpdate = 17,
        /// <summary>
        /// {0} 資料庫程序 {1} 執行錯誤。
        /// </summary>
        RunProcedureError = 18,
        /// <summary>
        /// 伺服機內容錯誤 : {0}
        /// </summary>
        Fail = 99999,
    }
}
