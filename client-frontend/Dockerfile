FROM node:14

ARG HOST

#ENV REACT_APP_HOST=$HOST

#RUN echo $REACT_APP_HOST

WORKDIR /usr/src/app

COPY package*.json ./

RUN npm install
RUN npm install -g serve

# Bundle app source
COPY . .

RUN npm run-script build

EXPOSE 8080

CMD [ "serve" , "-d", "-s", "build", "-p", "8080"]