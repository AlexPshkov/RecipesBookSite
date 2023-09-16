import {User} from "../objects/User";

export interface ChangeUserDataRequest extends User {
  id: string;
  password: string;
  description: string;
  login: string;
  role: string;
  userName: string;
}
