import { Data } from '@angular/router';

export interface Photo {
    id:number;
    url:string;
    description:string;
    dataAdded:Data;
    isMain:boolean;
}
