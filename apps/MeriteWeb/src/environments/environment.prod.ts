import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Merite',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:7600/',
    redirectUri: baseUrl,
    clientId: 'Merite_Angular',
    clientSecret: '1q2w3e*',
    responseType: 'code',
    scope: 'offline_access MeriteIdentityService MeriteAdministration MeriteSaaS',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:7500',
      rootNamespace: 'Merite',
    },
  },
} as Environment;
