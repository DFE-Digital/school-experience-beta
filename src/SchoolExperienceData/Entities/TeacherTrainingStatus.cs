namespace SchoolExperienceData.Entities
{
    public enum TeacherTrainingStatus
    {
        None,

        /// <summary>
        /// I'm thinking about teaching and want to find out more
        /// </summary>
        Thinking,

        /// <summary>
        /// I want to become a teacher
        /// </summary>
        WantToBecome,

        /// <summary>
        /// I've applied for teacher training.
        /// </summary>
        Applied,

        /// <summary>
        /// I've been accepted on teacher training.
        /// </summary>
        Accepted,
    }
}
