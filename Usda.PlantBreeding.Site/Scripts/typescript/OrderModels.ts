import * as Models from './Models'

export interface Order {
    Id?: number;
    Year?: number;
    Name?: string;
    GenusId: number;
    GrowerId?: number;
    Grower?: Grower;
    Location?: Location;
    GrowerName?: string;
    Note?: string | "";
    LocationId?: number;
    LocationName?: string;
    LocationAddress?: string;
    MTARequestedProp?: boolean;
    MTARequestedTest?: boolean;
    MTAComplete?: string;
}


export interface Location {
    Name?: string;
    Id?: number;
    Retired?: boolean;
    Description?: string;
    PrimaryContactId?: number;
    PrimaryContactName?: string;
    Address?: string;
    MapFlag: boolean;
}
export interface Grower {
    Id: number;
    FirstName: string;
    LastName: string;
    Email: string;
    WorkPhone: string;
    MailingName: string;
    MobilePhone: string;
    CreatedDate: string;
    FullName: string;
}

export interface OrderProduct {
    Id?: number;
    GenotypeId?: number;
    GenotypeName?: string;
    Quantity?: number;
    MaterialId?: number;
    OrderId?: number;
    VirusTested?: string;
    Note?: string;
    GenotypeVM?: Models.GenotypeVM;
    DateSent?: string;
}

export interface Material {
    Id: number;
    Name: string;
    Value: string;
}
