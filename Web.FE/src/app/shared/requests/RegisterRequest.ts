export class RegisterRequest {

  public name: string
  public login: string;
  public password: string;

  constructor(name: string, login: string, password: string) {
    this.name = name;
    this.login = login;
    this.password = password;
  }
}
