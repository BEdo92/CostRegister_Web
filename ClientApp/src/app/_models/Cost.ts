export interface Cost {
    date: Date;
    costAmount: number;
    category: string;
    shop: string;
    additionalInformation?: string;
}
