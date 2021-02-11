import { Injectable } from '@angular/core';

import * as _ from 'lodash';
import { LoDashStatic } from 'lodash';
import * as moment from 'moment';

@Injectable()
export class UtilityService {
    public lodash: LoDashStatic = _;
    public moment = moment;

    constructor() {
        
    }
}