# ==== CONFIGURE =====
FROM node:18-alpine as build
# Check https://github.com/nodejs/docker-node/tree/b4117f9333da4138b03a546ec926ef50a31506c3#nodealpine to understand why libc6-compat might be needed.
RUN apk add --no-cache libc6-compat

WORKDIR /app

# ==== BUILD =====    
COPY . ./
RUN --mount=type=cache,id=yarn,target=/usr/local/share/.cache/yarn/v6 yarn --frozen-lockfile
RUN yarn build

# ==== RUN =======
FROM nginx:stable-alpine
WORKDIR /usr/share/nginx/html
RUN rm -rf ./*

COPY --from=build /app/build .
RUN rm /etc/nginx/conf.d/default.conf
COPY nginx.conf /etc/nginx/conf.d
EXPOSE 80
ENTRYPOINT ["nginx", "-g", "daemon off;"]