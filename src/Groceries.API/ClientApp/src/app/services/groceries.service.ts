import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { BaseService } from './base.service';
import { grocery } from '../models/grocery';

@Injectable({
  providedIn: 'root'
})
export class GroceriesService extends BaseService {

  constructor(http: HttpClient) {
    super(http);
  }
  Get() {
    return this.get('');
  }
  Post(model: grocery[]) {
    return this.post('',model)
  }
  Put(model: grocery) {
    return this.put('', model.fruit, model)
  } 

}
