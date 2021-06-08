export interface AddressInfo {
    name: string;
    street: string;
    houseNumber: string;
    postalCode: string;
    city: string;
    country: string;
}

export interface ParcelStateInfo {
    name: string;
    description: string;
    created: string;
}

export interface ParcelInfo {
    id: string;
    returnAddress: AddressInfo;
    deliveryAddress: AddressInfo;
    states?: ParcelStateInfo[];
}

export interface StateInfo {
    id: number;
    name: string;
    description: string;
}
