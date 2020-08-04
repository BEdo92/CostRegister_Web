export interface CostPlan {
    typeOfCostPlan: string;
    categoryName: string;
    costPlanned: number;
    dateOfPlan: Date;
    planAdditionalInformation?: string;
}
