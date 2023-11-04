import react from '@vitejs/plugin-react';
import { resolve } from 'path';
import pic from 'picocolors';
import { defineConfig, loadEnv, splitVendorChunkPlugin } from 'vite';
import checker from 'vite-plugin-checker';
import { name } from './package.json';

function srcPath(subdir: string) {
  return resolve(__dirname, subdir);
}

export default defineConfig(({ command, mode }) => {
  process.env = { ...process.env, ...loadEnv(mode, process.cwd()) };
  return {
    plugins:
      command === 'serve'
        ? [react(), checker({ typescript: true })]
        : [
            {
              name: 'package-name',
              configResolved(config) {
                const packageName = pic.gray(`Building package ${pic.green(name)}`);
                config.logger.info(packageName);
              },
            },
            splitVendorChunkPlugin(),
            react(),
            checker({ typescript: true }),
          ],
    root: '.',
    resolve: {
      alias: {
        '@': srcPath('./src'),
        lib: srcPath('./src/lib'),
        utils: srcPath('./src/utils'),
        public: srcPath('./src/public'),
        features: srcPath('./src/features'),
        providers: srcPath('./src/providers'),
      },
    },
    css: {
      modules: {
        localsConvention: 'camelCase',
      },
    },
    server: {
      port: 3000,
      strictPort: true,
    },
    publicDir: './public',
    build: {
      assetsDir: '.',
      outDir: './build',
      emptyOutDir: true,
      manifest: false,
      rollupOptions: {
        input: {
          admin_postgresql_monitoring: './index.html',
        },
        output: {
          assetFileNames: 'index.[ext]',
          entryFileNames: '[name]_bundle.js',
          chunkFileNames: 'vendor_bundle.js',
        },
      },
    },
  };
});
