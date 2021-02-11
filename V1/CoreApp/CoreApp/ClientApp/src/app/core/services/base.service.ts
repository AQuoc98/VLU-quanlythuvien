import { ApiService } from './api.service';

export class BaseService {

    constructor(public apiService: ApiService, protected apiName: string) {

    }

    getAll() {
        return this.apiService.get(`${this.apiName}/GetAll`);
    }

    getById(id: string) {
        return this.apiService.get(`${this.apiName}/GetById/${id}`)
    }

    delete(models: any) {
        return this.apiService.post(`${this.apiName}/Delete`, models);
    }

    update(model: any) {
        return this.apiService.post(`${this.apiName}/Update`, model);
    }

    update2(model: any) {
      return this.apiService.post(`${this.apiName}/EditProfile`, model);
  }

    updates(models: any[]) {
        return this.apiService.post(`${this.apiName}/Updates`, models);
    }

    insert(model: any) {
        return this.apiService.post(`${this.apiName}/Insert`, model);
    }

    inserts(model: any[]) {
        return this.apiService.post(`${this.apiName}/Inserts`, model);
    }
}
