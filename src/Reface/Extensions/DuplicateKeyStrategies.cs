namespace Reface
{
    /// <summary>
    /// 产生重复键时的处理策略
    /// </summary>
    public enum DuplicateKeyStrategies
    {
        /// <summary>
        /// 跳过，将不对重复键的项进行任何处理
        /// </summary>
        Leap,
        /// <summary>
        /// 覆盖，将旧值替换掉
        /// </summary>
        Recover,
        /// <summary>
        /// 错误，将会抛出异常
        /// </summary>
        Error
    }
}
