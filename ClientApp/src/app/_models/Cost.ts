export interface Cost {
    id: number;
    dateOfCost: Date;
    amountOfCost: number;
    categoryName: string;
    shopName: string;
    additionalInformation?: string;
}
