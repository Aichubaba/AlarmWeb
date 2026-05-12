import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  stages: [
    { duration: '30s', target: 10 }, // разгон до 10 пользователей
    { duration: '1m', target: 10 },  // удержание 10 пользователей
    { duration: '30s', target: 0 },  // спад до 0
  ],
  thresholds: {
    http_req_duration: ['p(95)<500'], // 95% запросов должны быть быстрее 500 мс
  },
};

export default function () {
  const url = 'http://localhost:5159/';
  const res = http.get(url, { headers: { 'Accept': 'text/html' } });

  check(res, {
    'status is 200': (r) => r.status === 200,
    'response time < 1s': (r) => r.timings.duration < 1000,
  });

  sleep(1);
}