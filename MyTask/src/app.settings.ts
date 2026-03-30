export interface IEnvVars {
  ENV: string;
  API_URL: string;
}

export class AppSettings {
  public static env_vars: IEnvVars;
}
