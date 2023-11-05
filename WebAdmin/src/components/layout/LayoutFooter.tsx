import { ActionIcon, Anchor, AppShell, Flex, Text, Tooltip, rem } from '@mantine/core';
import { IconBrandTelegram } from '@tabler/icons-react';

export const LayoutFooter: React.FC = () => {
  return (
    <AppShell.Footer>
      <Flex px={120} h="100%" justify="space-between" align="center">
        <Text>Since 2023</Text>
        <Tooltip label="Telegram Monitoring Bot">
          <Anchor target="_blank" href={import.meta.env.VITE_TELEGRAM_BOT_URL}>
            <ActionIcon size="lg" color="gray" variant="subtle">
              <IconBrandTelegram style={{ width: rem(18), height: rem(18) }} stroke={1.5} />
            </ActionIcon>
          </Anchor>
        </Tooltip>
      </Flex>
    </AppShell.Footer>
  );
};
