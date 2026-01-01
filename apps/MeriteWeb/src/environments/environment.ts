import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'Merite',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:7600/',
    redirectUri: baseUrl,
    clientId: 'Merite_Angular',
    responseType: 'code',
    scope: 'MeriteIdentityService MeriteAdministration MeriteSaaS',
    requireHttps: false,
  },
  apis: {
    default: {
      url: 'https://localhost:7500',
      rootNamespace: 'Merite',
    },
  },
} as Environment;
