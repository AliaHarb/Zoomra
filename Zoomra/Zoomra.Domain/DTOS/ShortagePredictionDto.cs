// الرد الكامل اللي هييجي من الـ AI
public class ShortageResponseDto
{
    public string Status { get; set; }
    public List<ShortagePredictionDto> Predictions { get; set; }
    public int TotalPredictions { get; set; }
    public DateTime Timestamp { get; set; }
}


public class ShortagePredictionDto
{
    public string HospitalId { get; set; }
    public string BloodType { get; set; }
    public double RiskScore { get; set; }
    public string RiskLevel { get; set; } // CRITICAL, HIGH, etc.
    public int DaysToShortage { get; set; }
    public string ExpectedShortageDate { get; set; } // الـ AI بيبعتها String (YYYY-MM-DD)
}