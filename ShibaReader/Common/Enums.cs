namespace ShibaReader.Common
{
    class Enums
    {
        public enum DaysOfWeek 
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }

        public enum JobStatus
        {
            Failure = 0,
            Success = 1,
            Done = 2,
            Terminated = 3,
            Notrunning = 4
        }

        public enum Permission
        {
            GroupExecute,
            GroupEdit,
            AuthorizedExecute,
            AuthorizedEdit,
            WorldExecute,
            WorldEdit,
            None
        }

        public enum SendAlert
        {
            Yes,
            No,
            Failure
        }

        public enum NotificationAlarmType
        {
            MINRUNALARM,
            JOBFAILURE,
            MAX_RETRYS,
            STARTJOBFAIL,
            EVENT_HDLR_ERROR,
            EVENT_QUE_ERROR,
            JOBNOT_ONICEHOLD,
            MAXRUNALARM,
            RESOURCE,
            MISSING_HEARTBEAT,
            CHASE,
            DATABASE_COMM,
            VERSION_MISMATCH,
            DUPLICATE_EVENT,
            INSTANCE_UNAVAILABLE,
            AUTO_PING,
            EXTERN_DEPS_ERROR,
            SERVICEDESK_FAILURE,
            UNINOTIFY_FAILURE,
            CPI_JOBNAME_INVALID,
            CPI_UNAVAILABLE,
            MUST_START_ALARM,
            MUST_COMPLETE_ALARM,
            WAIT_REPLY_ALARM,
            KILLJOBFAIL,
            SENDSIGFAIL,
            REPLY_RESPONSE_FAIL,
            RETURN_RESOURCE_FAIL,
            RESTARTJOBFAIL,
            JOBNOT_ONNOEXEC,
            QUEUEDJOB_STARTFAIL,
            SUSPENDJOBFAIL,
            RESUMEJOBFAIL,
            ALL
        }
    }
}
