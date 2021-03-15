
const host = 'https://ph96u94icf.execute-api.us-east-2.amazonaws.com/Prod';
// const host = process.env.REACT_APP_HOST || process.env.HOST;


const getHeaders = () => {
  const user = localStorage.getItem('user');
  const token = user ? JSON.parse(user).token : '';

  return {
    'Content-Type': 'application/json',
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Request-Headers': '*',
    'x-api-token': token,
  }
}


const post = <T extends {}>(url: string, data: T) => fetch(`${host}/${url}`, {
  method: 'POST',
  mode: 'cors',
  headers: getHeaders(),
  referrerPolicy: 'no-referrer',
  body: JSON.stringify(data),
});

export const get = (url: string, params?: string) =>
  fetch(`${host}/${url}/${params ? '?'+params : ''}`, {
    method: 'GET',
    headers: getHeaders(),
    referrerPolicy: 'no-referrer',
  })

// export const del = ({ url, params }: ListRequestConfig) =>
//   fetch(`${host}/${url}/${params ? '?'+params : ''}`, {
//     method: 'DELETE',
//     headers: getHeaders(),
//     referrerPolicy: 'no-referrer',
//   })

// TODO: can refactor these and place 'em all in one
export async function postRequest(
  url: string,
  data: any,
) {
  const response = await post(url, data);
  const parsed = await response.json();
  if (parsed.status === 403) throw new Error(parsed.status);
  return parsed;
}

export async function getRequest(
  url: string,
  params: string
) {
  const response = await get(url, params);
  const parsed = await response.json();
  if (parsed.status === 403) throw new Error(parsed.status);
  return parsed;
}