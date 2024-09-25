import { Option } from "./option.model";

export interface Question {
    Id: number;
    Text: string;
    Options: Option[];
}